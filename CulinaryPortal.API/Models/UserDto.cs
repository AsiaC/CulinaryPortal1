using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Models
{
    public class UserDto //model do zwrotu
    {
        public int Id { get; set; } //??czy tego tu potrzebuje?

        public string Name { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        //dodane
        public CookbookDto Cookbook { get; set; }

        public IList<RecipeDto> Recipes { get; set; }

    }
}
