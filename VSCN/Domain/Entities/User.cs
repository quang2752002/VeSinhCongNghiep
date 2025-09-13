using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User: IdentityUser
    {

        // --- Custom User Profile Information ---

        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        public DateTime? Dob { get; set; } // Date of Birth

        [MaxLength(10)]
        public string? Sex { get; set; }

        public string? Avatar { get; set; } // URL to the user's avatar image

        /// <summary>
        /// A flag to indicate if the user account is active. 
        /// Allows for soft-banning or deactivating users without deleting them.
        /// </summary>
        public bool IsActive { get; set; } = true;

        // --- Authentication and Session Management ---

        /// <summary>
        /// Stores the current valid Refresh Token for the user.
        /// This is used to obtain new access tokens without requiring re-login.
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// The expiration date and time for the current Refresh Token.
        /// After this time, the user must log in again.
        /// </summary>
        public DateTime RefreshTokenExpiryTime { get; set; }

        // --- Navigation Properties for Relationships ---

        /// <summary>
        /// A collection of orders placed by this user.
        /// This is a navigation property used by Entity Framework Core to define the one-to-many relationship.
        /// </summary>
        public virtual ICollection<Order>? Orders { get; set; }

    }
}
