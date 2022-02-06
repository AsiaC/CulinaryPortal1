using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.Domain.Entities
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public IList<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
