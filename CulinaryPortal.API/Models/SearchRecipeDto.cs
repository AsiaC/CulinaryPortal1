using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Models
{
    public class SearchRecipeDto
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? DifficultyLevelId { get; set; }
        public int? PreparationTimeId { get; set; }                
    }
}
