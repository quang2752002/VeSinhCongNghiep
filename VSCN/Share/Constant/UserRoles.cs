using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Constant
{
    /// <summary>
    /// Contains constant strings for user roles to ensure consistency across the application.
    /// </summary>
    public static class UserRoles
    {
        /// <summary>
        /// The role for administrators with full permissions.
        /// </summary>
        public const string Admin = "Admin";

        /// <summary>
        /// The default role for registered users.
        /// </summary>
        public const string Customer = "Customer";
    }
}