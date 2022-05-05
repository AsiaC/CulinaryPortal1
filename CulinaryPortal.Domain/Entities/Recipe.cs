using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CulinaryPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.Domain.Entities
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }        

        [MaxLength(100)]
        public string Description { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public DifficultyLevel DifficultyLevel { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public PreparationTime PreparationTime { get; set; }

        public IList<Instruction> Instructions { get; set; }
        
        public IList<Photo> Photos { get; set; }

        public IList<CookbookRecipe> CookbookRecipes { get; set; }// = new List<CookbookRecipe>();

        public IList<RecipeIngredient> RecipeIngredients { get; set; }

        public IList<Rate> Rates { get; set; }
    }
}
