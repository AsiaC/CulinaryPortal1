using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.Domain.Entities
{
    public class User : IdentityUser<int>
    {  
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }    
        public Cookbook Cookbook { get; set; }
        public IList<Recipe> Recipes { get; set; }        
        public IList<Rate> Rates { get; set; }
        public IList<ShoppingList> ShoppingLists { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
