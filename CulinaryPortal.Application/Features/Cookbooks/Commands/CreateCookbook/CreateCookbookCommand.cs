using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Cookbooks.Commands.CreateCookbook
{
    public class CreateCookbookCommand : IRequest<CookbookDto>
    {        
        public string Name { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        //public User User { get; set; }
        //public IList<RecipeDto> Recipes { get; set; } = new List<RecipeDto>();
        public IList<CookbookRecipeDto> CookbookRecipes { get; set; }
    }
}
