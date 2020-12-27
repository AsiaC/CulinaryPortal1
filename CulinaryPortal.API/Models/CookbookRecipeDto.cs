using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Models
{
    public class CookbookRecipeDto
    {
        public RecipeDto Recipe { get; set; }
        public string Note { get; set; }
    }
}
