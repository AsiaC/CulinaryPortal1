using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Models
{
    public class CookbookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        //public User User { get; set; }
        public IList<RecipeDto> Recipes { get; set; } = new List<RecipeDto>();

    }
}
