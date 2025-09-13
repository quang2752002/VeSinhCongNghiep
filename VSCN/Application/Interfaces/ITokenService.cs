using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Interfaces
{
    /// <summary>
    /// Defines the contract for a service that handles JWT token generation and validation.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a short-lived JWT Access Token based on a user's claims.
        /// </summary>
        /// <param name="claims">A collection of claims for the user.</param>
        /// <returns>A JWT access token as a string.</returns>
        string GenerateAccessToken(IEnumerable<Claim> claims);

        /// <summary>
        /// Generates a cryptographically secure, long-lived Refresh Token.
        /// </summary>
        /// <returns>A random string representing the refresh token.</returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Validates an expired access token's signature and extracts its claims.
        /// This is used for the refresh token flow.
        /// </summary>
        /// <param name="token">The expired access token.</param>
        /// <returns>A ClaimsPrincipal containing the user's identity and claims.</returns>
        /// <exception cref="SecurityTokenException">Thrown if the token is invalid for any reason other than expiry.</exception>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}