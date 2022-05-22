using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryPortal.Application.Models;
using MediatR;

namespace CulinaryPortal.Application.Features.Recipes.Queries.GetRecipesList
{
    public class GetRecipesListQuery : IRequest<List<RecipeDto>>
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? DifficultyLevelId { get; set; }
        public int? PreparationTimeId { get; set; }
        public int? UserId { get; set; }
        public int? Top { get; set; }
    }
}
