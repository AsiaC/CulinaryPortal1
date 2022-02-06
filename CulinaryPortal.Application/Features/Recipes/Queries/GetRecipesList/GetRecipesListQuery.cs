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
    {//public class GetShoppingListsListQuery : IRequest<List<ShoppingListDto>>
    }
}
