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

namespace CulinaryPortal.Application.Features.ShoppingLists.Commands.CreateShoppingList
{
    public class CreateShoppingListCommandHandler : IRequestHandler<CreateShoppingListCommand, ShoppingListDto>
    {
        private readonly IAsyncRepository<ShoppingList> _shoppingListRepository;
        private readonly IMapper _mapper;
        public CreateShoppingListCommandHandler(IMapper mapper, IAsyncRepository<ShoppingList> shoppingListRepository)
        {
            _mapper = mapper;
            _shoppingListRepository = shoppingListRepository;
        }
        public async Task<ShoppingListDto> Handle(CreateShoppingListCommand request, CancellationToken cancellationToken)
        {            
            var @event = _mapper.Map<ShoppingList>(request);

            @event = await _shoppingListRepository.AddAsync(@event);

            var objestToReturn = _mapper.Map<ShoppingListDto>(@event);

            return objestToReturn;
        }
    }    
}
