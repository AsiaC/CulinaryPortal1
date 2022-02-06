using AutoMapper;
using CulinaryPortal.Application.Models;
using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.ShoppingLists.Queries.GetShoppingListsList
{
    class GetShoppingListsListQueryHandler : IRequestHandler<GetShoppingListsListQuery, List<ShoppingListDto>>
    {
        private readonly IAsyncRepository<ShoppingList> _shoppingListRepository;
        private readonly IMapper _mapper;

        public GetShoppingListsListQueryHandler(IMapper mapper, IAsyncRepository<ShoppingList> shoppingListRepository)
        {
            _mapper = mapper;
            _shoppingListRepository = shoppingListRepository;
        }
        public async Task<List<ShoppingListDto>> Handle(GetShoppingListsListQuery request, CancellationToken cancellationToken)
        {
            var allShoppingLists = (await _shoppingListRepository.ListAllAsync()).OrderBy(x => x.Id);
            return _mapper.Map<List<ShoppingListDto>>(allShoppingLists);
        }
    }
}
