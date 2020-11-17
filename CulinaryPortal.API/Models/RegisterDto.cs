using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Models
{
    public class RegisterDto //model do tworzenia
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [MaxLength(20)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
