using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    /// <summary>
    /// Represents the data transfer object returned after a successful authentication attempt.
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>
        /// Indicates if the authentication was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// A message providing details about the authentication result.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The JWT Access Token, present only on success.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// The Refresh Token, present only on success.
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// A list of roles assigned to the user, used by the client for role-based logic.
        /// </summary>
        public List<string>? Roles { get; set; }
    }
}
