using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class JwtDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public JwtDTO(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }
}
