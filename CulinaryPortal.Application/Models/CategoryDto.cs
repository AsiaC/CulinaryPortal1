using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Models
{
    public class CategoryDto //TODO UPDATE CONTROLLER,REPOSITORY, HANDLERS
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<RecipeDto> Recipes { get; set; }
    }
}
