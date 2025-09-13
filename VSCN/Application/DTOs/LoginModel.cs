using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    /// <summary>
    /// Represents the data transfer object for user login.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// The user's username.
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
