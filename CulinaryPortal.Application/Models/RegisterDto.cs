using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Models
{
    public class RegisterDto
    {
        //[Required]
        //[MaxLength(30)]
        public string FirstName { get; set; }
        //[Required]
        //[MaxLength(30)]
        public string LastName { get; set; }
        //[Required]
        //[MaxLength(30)]
        public string Username { get; set; }
        //[Required]
        //[MaxLength(20)]
        //[EmailAddress]
        public string Email { get; set; }
        //[Required]
        public string Password { get; set; }
    }
}
