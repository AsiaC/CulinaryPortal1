using AutoMapper;
using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.ShoppingLists.Commands.UpdateShoppingList
{
    public class UpdateShoppingListCommandHandler : IRequestHandler<UpdateShoppingListCommand>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;
        public UpdateShoppingListCommandHandler(IMapper mapper, IShoppingListRepository shoppingListRepository)
        {
            _mapper = mapper;
            _shoppingListRepository = shoppingListRepository;
        }
        public async Task<Unit> Handle(UpdateShoppingListCommand request, CancellationToken cancellationToken)
        {  
            var objectToUpdate = await _shoppingListRepository.GetShoppingListWithDetailsAsync(request.Id);
            if (objectToUpdate == null)
            {
                throw new Exception("Server error while updating the shopping list. Object not found.");
            }            
            _mapper.Map(request, objectToUpdate, typeof(UpdateShoppingListCommand), typeof(ShoppingList));

            await _shoppingListRepository.UpdateAsync(objectToUpdate);

            return Unit.Value;
        }
    }
}
