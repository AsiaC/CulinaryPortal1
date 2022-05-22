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
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;

        public GetShoppingListsListQueryHandler(IMapper mapper, IShoppingListRepository shoppingListRepository)
        {
            _mapper = mapper;
            _shoppingListRepository = shoppingListRepository;
        }
        public async Task<List<ShoppingListDto>> Handle(GetShoppingListsListQuery request, CancellationToken cancellationToken)
        {
            var allShoppingLists = (await _shoppingListRepository.GetShoppingListsWithDetailsAsync(request.UserId)).OrderBy(x => x.Id);
            return _mapper.Map<List<ShoppingListDto>>(allShoppingLists);
        }
    }
}
