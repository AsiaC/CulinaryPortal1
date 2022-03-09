using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Recipes.Queries.GetRecipePhotos
{
    public class GetRecipePhotosQuery : IRequest<List<PhotoDto>>
    {
        public int RecipeId { get; set; }
    }
}
