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

namespace CulinaryPortal.Application.Features.Ingredients.Commands.CreateIngredient
{
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, IngredientDto>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public CreateIngredientCommandHandler(IMapper mapper, IIngredientRepository ingredientRepository)
        {
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<IngredientDto> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var @event = _mapper.Map<Ingredient>(request);

            @event = await _ingredientRepository.AddAsync(@event);

            var objestToReturn = _mapper.Map<IngredientDto>(@event);

            return objestToReturn;
        }
    }
}
