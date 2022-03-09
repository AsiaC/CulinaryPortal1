using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Cookbooks.Commands.UpdateCookbook
{
    public class UpdateCookbookCommand : IRequest
    {
        //public RecipeDto Recipe { get; set; } //todo sprawdz
        public bool IsRecipeAdded { get; set; }
        public int RecipeId { get; set; }
        public int CookbookId { get; set; }
        public string Note { get; set; }

        public int UserId { get; set; } //dod // CZY JA TEGO POTRZEBUJE?
    }
}
