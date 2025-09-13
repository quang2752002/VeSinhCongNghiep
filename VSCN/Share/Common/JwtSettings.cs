using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Common
{
    
        /// <summary>
        /// Represents the JWT settings configured in appsettings.json.
        /// This class is used for strongly-typed configuration binding.
        /// </summary>
        public class JwtSettings
        {
            /// <summary>
            /// The issuer of the token (e.g., your API domain).
            /// </summary>
            public string Issuer { get; set; }

            /// <summary>
            /// The audience of the token (e.g., your client/frontend domain).
            /// </summary>
            public string Audience { get; set; }

            /// <summary>
            /// The secret key used to sign and validate the JWT.
            /// MUST be long and complex.
            /// </summary>
            public string Secretkey { get; set; }

            /// <summary>
            /// The duration of the access token in minutes.
            /// </summary>
            public int AccessTokenDurationInMinutes { get; set; }

            /// <summary>
            /// The duration of the refresh token in days.
            /// </summary>
            public int RefreshTokenDurationInDays { get; set; }
        }
    
}
