using CulinaryPortal.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Models
{
    public class RecipeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Rate { get; set; } //DEFAULT 0

        public string Description { get; set; }

        public int? UserId { get; set; }
        //public User User { get; set; }

        public IList<InstructionDto> Instructions { get; set; }

        //public IList<PhotoDto> Photos { get; set; }

        //public IList<CookbookDto> Cookbooks { get; set; }

        //public IList<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
