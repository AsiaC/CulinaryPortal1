using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Models
{
    public class RegisterDto //do tworzenia
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
