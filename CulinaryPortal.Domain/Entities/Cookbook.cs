using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.Domain.Entities
{
    [Table("Cookbooks")]
    public class Cookbook
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }     

        public int UserId { get; set; }
        public User User { get; set; }

        public IList<CookbookRecipe> CookbookRecipes { get; set; }// = new List<CookbookRecipe>();
    }
}
