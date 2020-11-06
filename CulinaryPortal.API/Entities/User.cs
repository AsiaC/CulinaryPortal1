using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class User
    {
        //[Key]
        //public Guid Id { get; set; } //not to do is use the GUID column as the clustering key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //id INT NOT NULL PRIMARY KEY IDENTITY
        public int Id { get; set; }

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
        public string Email { get; set; }
        //string/ zachashowane/password
        //string / enum / role
    }
}
