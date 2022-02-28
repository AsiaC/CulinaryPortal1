using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Models
{
    public class CookbookRecipeDto
    {
        public RecipeDto Recipe { get; set; }
        public int RecipeId { get; set; }
        public int CookbookId { get; set; }    
        public int UserId { get; set; } //dod // CZY JA TEGO POTRZEBUJE?
    }
}
