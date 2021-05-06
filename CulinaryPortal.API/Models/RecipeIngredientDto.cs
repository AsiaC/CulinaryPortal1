using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Models
{
    public class RecipeIngredientDto
    {
        //public int RecipeId { get; set; }
        //public int IngredientId { get; set; }
        public IngredientDto Ingredient { get; set; }

        //public int MeasureId { get; set; }
        public MeasureDto Measure { get; set; }

        public decimal Quantity { get; set; }
        public string IngredientName { get; set; }
        public string MeasureName { get; set; }
    }
}
