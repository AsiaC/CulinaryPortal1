using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class CookbookRecipe
    {
        public int CookbookId { get; set; }
        
        public int RecipeId { get; set; }
        
        public string Note { get; set; }
    }
}
