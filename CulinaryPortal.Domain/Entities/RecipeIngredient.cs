using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.Domain.Entities
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public int MeasureId { get; set; }
        public Measure Measure { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Quantity { get; set; }
    }
}
