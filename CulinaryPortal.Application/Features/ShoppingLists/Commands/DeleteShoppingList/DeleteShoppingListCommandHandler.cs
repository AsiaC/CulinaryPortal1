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

namespace CulinaryPortal.Application.Features.ShoppingLists.Commands.DeleteShoppingList
{
    public class DeleteShoppingListCommandHandler : IRequestHandler<DeleteShoppingListCommand>
    {
        private readonly IAsyncRepository<ShoppingList> _shoppingListRepository;
        public DeleteShoppingListCommandHandler(IMapper mapper, IAsyncRepository<ShoppingList> shoppingListRepository)
        {            
            _shoppingListRepository = shoppingListRepository;
        }
        public async Task<Unit> Handle(DeleteShoppingListCommand request, CancellationToken cancellationToken)
        {
            var objectToDelete = await _shoppingListRepository.GetByIdAsync(request.Id);

            if (objectToDelete == null)
            {
                //TODO exceptions
                //throw new NotFoundException(nameof(Event), request.EventId);
            }

            await _shoppingListRepository.DeleteAsync(objectToDelete);

            return Unit.Value;
        }
    }
}
