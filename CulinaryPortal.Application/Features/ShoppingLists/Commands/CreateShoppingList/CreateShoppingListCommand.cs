using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.ShoppingLists.Commands.CreateShoppingList
{
    public class CreateShoppingListCommand : IRequest<ShoppingListDto> 
    {        
        public string Name { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public IList<ListItemDto> Items { get; set; }
    }    
}
