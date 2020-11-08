using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class Cookbook
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
        
        
        [MaxLength(180)]
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public IList<CookbookRecipe> CookbookRecipes { get; set; }
    }
}
