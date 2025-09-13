using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    /// <summary>
    /// Represents the data transfer object for user registration.
    /// </summary>
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        // Các trường này có thể không bắt buộc, nên ta dùng kiểu string? (nullable)
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
