using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Users.Queries.GetUserRecipes
{
    public class GetUserRecipesQuery : IRequest<List<RecipeDto>>
    {
        public int UserId { get; set; }
    }
}
