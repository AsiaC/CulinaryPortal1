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

namespace CulinaryPortal.Application.Features.Recipes.Commands.UpdateRecipe
{
    public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand>
    {
        private readonly IAsyncRepository<Recipe> _recipeRepository;
        private readonly IMapper _mapper;
        public UpdateRecipeCommandHandler(IMapper mapper, IAsyncRepository<Recipe> recipeRepository)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }

        public async Task<Unit> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var objectToUpdate = await _recipeRepository.GetByIdAsync(request.Id);

            if (objectToUpdate == null)
            {
                //TODO DODAC EXCEPTIONS
                //throw new NotFoundException(nameof(Event), request.EventId);
            }
            //todo validacja?
            var @event = _mapper.Map<Recipe>(request);

            await _recipeRepository.UpdateAsync(@event);

            return Unit.Value;
        }
    }
}
