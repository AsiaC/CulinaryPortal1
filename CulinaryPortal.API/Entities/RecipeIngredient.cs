using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        
        public int IngredientId { get; set; }
        
        public int MeasureId { get; set; }
        
        public int Quantity { get; set; }
    }
}
