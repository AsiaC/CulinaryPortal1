using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Models
{
    public class ShoppingListDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public IList<ListItemDto> Items { get; set; }
    }
}
