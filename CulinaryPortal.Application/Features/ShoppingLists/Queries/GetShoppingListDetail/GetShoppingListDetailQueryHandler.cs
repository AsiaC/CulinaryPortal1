using AutoMapper;
using CulinaryPortal.Application.Models;
using CulinaryPortal.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.ShoppingLists.Queries.GetShoppingListDetail
{
    public class GetShoppingListDetailQueryHandler : IRequestHandler<GetShoppingListDetailQuery, ShoppingListDto>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;
        public GetShoppingListDetailQueryHandler(IMapper mapper, IShoppingListRepository shoppingListRepository)
        {
            _mapper = mapper;
            _shoppingListRepository = shoppingListRepository;
        }
        public async Task<ShoppingListDto> Handle(GetShoppingListDetailQuery request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListRepository.GetShoppingListWithDetailsAsync(request.Id);
            var shoppingListDetailDto = _mapper.Map<ShoppingListDto>(shoppingList);
            return shoppingListDetailDto;
        }
    }
}
