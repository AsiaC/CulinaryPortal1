using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Cookbooks.Queries.GetCookbookRecipes
{
    public class GetCookbookRecipesQuery : IRequest<List<RecipeDto>>
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? DifficultyLevelId { get; set; }
        public int? PreparationTimeId { get; set; }
        public int? UserId { get; set; }
    }    
}
