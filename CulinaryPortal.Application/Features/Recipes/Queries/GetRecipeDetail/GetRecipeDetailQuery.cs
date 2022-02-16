using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Recipes.Queries.GetRecipeDetail
{
    public class GetRecipeDetailQuery : IRequest<RecipeDto>
    {
        public int Id { get; set; }
    }
}
