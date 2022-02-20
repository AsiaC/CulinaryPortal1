using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.ShoppingLists.Commands.DeleteShoppingList
{
    public class DeleteShoppingListCommand : IRequest
    {
        public int Id { get; set; }
    }
}
