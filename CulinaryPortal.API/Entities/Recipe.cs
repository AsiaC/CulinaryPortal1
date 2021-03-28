using CulinaryPortal.API.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public double Rate { get; set; } //DEFAULT 0

        [MaxLength(100)]
        public string Description { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }
        public PreparationTime PreparationTime { get; set;  }

        public IList<Instruction> Instructions { get; set; }
        
        public IList<Photo> Photos { get; set; }

        public IList<CookbookRecipe> CookbookRecipes { get; set; }

        public IList<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
