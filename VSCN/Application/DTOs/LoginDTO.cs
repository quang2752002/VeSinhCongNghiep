using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LoginDTO
    {
        public string? UserName {  get; set; }
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }


        
        public string? Password { get; set; }
        [StringLength(50, MinimumLength = 1,
        ErrorMessage = "The first name must contain between 1 and 50 characters")]
        public string? FirstName { get; set; }
      
        [StringLength(50, MinimumLength = 1,
        ErrorMessage = "The last name must contain between 1 and 50 characters")]
        public string? LastName { get; set; }

        public DateTime? CreatedAt {  get; set; }


    }
}
