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
        private readonly IAsyncRepository<Ingredient> _ingredientRepository;
        private readonly IMapper _mapper;

        public CreateIngredientCommandHandler(IMapper mapper, IAsyncRepository<Ingredient> ingredientRepository)
        {
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<IngredientDto> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = _mapper.Map<Ingredient>(request);

            ingredient = await _ingredientRepository.AddAsync(ingredient);

            var objectToReturn = _mapper.Map<IngredientDto>(ingredient);

            return objectToReturn;
        }
    }
}
