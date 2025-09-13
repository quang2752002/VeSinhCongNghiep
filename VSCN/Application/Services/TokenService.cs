using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Share.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    /// <summary>
    /// Implements the ITokenService to provide JWT token generation and validation logic.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<TokenService> _logger;

        public TokenService(JwtSettings jwtSettings, ILogger<TokenService> logger)
        {
            _jwtSettings = jwtSettings;
            _logger = logger;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secretkey));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenDurationInMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            _logger.LogInformation("Generated new Access Token.");
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            _logger.LogInformation("Generated new Refresh Token.");
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secretkey)),
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                // Quan trọng: không kiểm tra token hết hạn ở đây, vì chúng ta đang xử lý một token đã hết hạn.
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            // Thêm một lớp bảo vệ: đảm bảo token được trả về có đúng thuật toán mã hóa
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.LogWarning("Attempted to refresh a token with an invalid algorithm.");
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}