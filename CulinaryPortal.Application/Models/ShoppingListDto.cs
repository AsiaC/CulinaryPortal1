using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Models
{
    public class ShoppingListDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; } //TODO czy ja tego potrzebuje
        public string UserName { get; set; }
        public IList<ListItemDto> Items { get; set; }
    }
}
