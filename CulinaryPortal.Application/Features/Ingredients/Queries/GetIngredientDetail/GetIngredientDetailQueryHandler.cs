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

namespace CulinaryPortal.Application.Features.Ingredients.Queries.GetIngredientDetail
{
    public class GetIngredientDetailQueryHandler : IRequestHandler<GetIngredientDetailQuery, IngredientDto>
    {
        private readonly IAsyncRepository<Ingredient> _ingredientRepository;
        private readonly IMapper _mapper;
        public GetIngredientDetailQueryHandler(IMapper mapper, IAsyncRepository<Ingredient> ingredientRepository)
        {
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<IngredientDto> Handle(GetIngredientDetailQuery request, CancellationToken cancellationToken)
        {
            var @ingredient = await _ingredientRepository.GetByIdAsync(request.Id);
            var ingredientDetailDto = _mapper.Map<IngredientDto>(@ingredient);
            return ingredientDetailDto;
        }
    }
}
