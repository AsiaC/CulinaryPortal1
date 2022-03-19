using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.Domain.Entities
{
    public class CookbookRecipe
    {
        public int CookbookId { get; set; }
        //public Cookbook Cookbook { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
