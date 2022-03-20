using CulinaryPortal.Application.Models;
using CulinaryPortal.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Recipes.Commands.UpdateRecipe
{
    public class UpdateRecipeCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public decimal TotalScore { get; set; }
        //public int RateValues { get; set; }
        //public int CountRates { get; set; }

        public string Description { get; set; }

        //public int? UserId { get; set; }
        //public User User { get; set; }
        //public string Author { get; set; }
        public int CategoryId { get; set; }
        //public string Category { get; set; }
        //public int CountCookbooks { get; set; }


        public DifficultyLevel DifficultyLevel { get; set; }
        //public string Difficulty { get; set; }
        public PreparationTime PreparationTime { get; set; }
        //public string Preparation { get; set; }

        public IList<InstructionDto> Instructions { get; set; }

        public IList<PhotoDto> Photos { get; set; }

        //public IList<CookbookDto> Cookbooks { get; set; }

        public IList<RecipeIngredientDto> RecipeIngredients { get; set; }

        //public IList<RateDto> Rates { get; set; }
    }
}
