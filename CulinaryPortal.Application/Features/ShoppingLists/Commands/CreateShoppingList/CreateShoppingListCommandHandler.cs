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
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;
        public CreateShoppingListCommandHandler(IMapper mapper, IShoppingListRepository shoppingListRepository)
        {
            _mapper = mapper;
            _shoppingListRepository = shoppingListRepository;
        }
        public async Task<ShoppingListDto> Handle(CreateShoppingListCommand request, CancellationToken cancellationToken)
        {            
            var shoppingList = _mapper.Map<ShoppingList>(request);

            shoppingList = await _shoppingListRepository.AddAsync(shoppingList);

            var objectToReturn = _mapper.Map<ShoppingListDto>(shoppingList);

            return objectToReturn;
        }
    }    
}
