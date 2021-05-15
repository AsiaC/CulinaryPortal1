using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Models
{
    public class RecipeDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public double Rate { get; set; } //DEFAULT 0

        public string Description { get; set; }

        public int? UserId { get; set; }
        //public User User { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }
        //public string Difficulty { get; set; }
        public PreparationTime PreparationTime { get; set; }
        //public string Preparation { get; set; }

        public IList<InstructionDto> Instructions { get; set; }

        public IList<PhotoDto> Photos { get; set; }

        //public IList<CookbookDto> Cookbooks { get; set; }

        public IList<RecipeIngredientDto> RecipeIngredients { get; set; }
    }
}
