using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    /// <summary>
    /// Represents the data transfer object for token refresh requests.
    /// It contains the expired access token and the valid refresh token.
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// The expired JWT Access Token.
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// The valid Refresh Token used to obtain a new access token.
        /// </summary>
        public string? RefreshToken { get; set; }
    }
}
