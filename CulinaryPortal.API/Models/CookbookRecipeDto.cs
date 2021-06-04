using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Models
{
    public class CookbookRecipeDto
    {
        //public RecipeDto Recipe { get; set; }
        public int RecipeId { get; set; }        
        public int CookbookId { get; set; }
        public string Note { get; set; }

        public int UserId { get; set; } //dod // CZY JA TEGO POTRZEBUJE?
    }
}
