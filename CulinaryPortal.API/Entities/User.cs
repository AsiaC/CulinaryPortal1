using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class User //: IdentityUser<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [MaxLength(20)]
        [EmailAddress]
        public string Email { get; set; }

        public Cookbook Cookbook { get; set; }

        public IList<Recipe> Recipes { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        //string/ zachashowane/password
        //string / enum / role
        public IList<Rate> Rates { get; set; }
        public IList<ShoppingList> ShoppingLists { get; set; }
    }
}
