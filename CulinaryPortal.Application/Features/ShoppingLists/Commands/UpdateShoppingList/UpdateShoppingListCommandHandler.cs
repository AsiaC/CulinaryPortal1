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
        {//todo update nie działa
            //var objectToUpdate = await _shoppingListRepository.GetByIdAsync(request.Id);

            //if (objectToUpdate == null)
            //{
                //TODO DODAC EXCEPTIONS
                //throw new NotFoundException(nameof(Event), request.EventId);
            //}
            //TODO co z items? validacja?
            var @event = _mapper.Map<ShoppingList>(request);
            
            await _shoppingListRepository.UpdateAsync(@event);

            return Unit.Value;
        }
    }
}
