using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Share.Common;
using Share.Constant;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Apis.Auth; // Đảm bảo đã cài gói NuGet: Google.Apis.Auth

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService,
            JwtSettings jwtSettings,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status409Conflict, new { Message = "User already exists!" });

            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return StatusCode(StatusCodes.Status409Conflict, new { Message = "Email already exists!" });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "User creation failed! Please check user details and try again.", Errors = result.Errors });

            // Gán role mặc định cho user mới
            await _userManager.AddToRoleAsync(user, UserRoles.Customer);

            return Ok(new { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (!user.IsActive)
                    return Unauthorized(new AuthResponseDto { IsSuccess = false, Message = "User account is locked." });

                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id)
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var accessToken = _tokenService.GenerateAccessToken(authClaims);
                var refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDurationInDays);
                await _userManager.UpdateAsync(user);

                return Ok(new AuthResponseDto
                {
                    IsSuccess = true,
                    Message = "Login successful",
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    Roles = userRoles.ToList() // Trả về role để frontend xử lý chuyển hướng
                });
            }
            return Unauthorized(new AuthResponseDto { IsSuccess = false, Message = "Invalid username or password" });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            if (tokenModel is null)
                return BadRequest(new AuthResponseDto { IsSuccess = false, Message = "Invalid client request" });

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;
            ClaimsPrincipal principal;

            try
            {
                principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthResponseDto { IsSuccess = false, Message = $"Invalid access token: {ex.Message}" });
            }

            var username = principal.Identity?.Name;
            if (username is null)
                return BadRequest(new AuthResponseDto { IsSuccess = false, Message = "Invalid access token: Could not get username" });

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                if (user != null)
                {
                    user.RefreshToken = null;
                    await _userManager.UpdateAsync(user);
                }
                return Forbid();
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList());
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDurationInDays);
            await _userManager.UpdateAsync(user);

            return Ok(new AuthResponseDto()
            {
                IsSuccess = true,
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeToken()
        {
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
                return BadRequest("Invalid token: Cannot determine user.");

            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("User not found.");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return Ok(new { Status = "Success", Message = "Token revoked successfully." });
        }

        [HttpPost("google-signin")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleSignIn([FromBody] GoogleLoginRequest request)
        {
            var googleClientId = _configuration["Authentication:Google:ClientId"];

            if (string.IsNullOrEmpty(googleClientId))
                return StatusCode(500, new AuthResponseDto { IsSuccess = false, Message = "Google ClientId is not configured on the server." });

            var validationSettings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { googleClientId }
            };

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, validationSettings);
            }
            catch (InvalidJwtException ex)
            {
                return BadRequest(new AuthResponseDto { IsSuccess = false, Message = $"Invalid Google token: {ex.Message}" });
            }

            var userEmail = payload.Email;
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                user = new User
                {
                    UserName = userEmail,
                    Email = userEmail,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    EmailConfirmed = true,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                    return StatusCode(500, new AuthResponseDto { IsSuccess = false, Message = "Failed to create user." });

                await _userManager.AddToRoleAsync(user, UserRoles.Customer);
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var accessToken = _tokenService.GenerateAccessToken(authClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDurationInDays);
            await _userManager.UpdateAsync(user);

            return Ok(new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Google login successful",
                Token = accessToken,
                RefreshToken = refreshToken,
                Roles = userRoles.ToList() // Trả về role để frontend xử lý chuyển hướng
            });
        }
    }

    // DTO này có thể đặt ở đây hoặc trong một file riêng
    public class GoogleLoginRequest
    {
        [Required]
        public string IdToken { get; set; }
    }
}