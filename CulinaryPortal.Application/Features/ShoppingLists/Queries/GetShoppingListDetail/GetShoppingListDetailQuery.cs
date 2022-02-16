using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.ShoppingLists.Queries.GetShoppingListDetail
{
    public class GetShoppingListDetailQuery : IRequest<ShoppingListDto>
    {
        public int Id { get; set; }
    }
}
