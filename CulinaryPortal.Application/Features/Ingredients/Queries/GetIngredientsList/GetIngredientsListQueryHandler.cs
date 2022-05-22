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

namespace CulinaryPortal.Application.Features.Ingredients.Queries.GetIngredientsList
{
    public class GetIngredientsListQueryHandler : IRequestHandler<GetIngredientsListQuery, List<IngredientDto>>
    {
        private readonly IAsyncRepository<Ingredient> _ingredientRepository;
        private readonly IMapper _mapper;
        public GetIngredientsListQueryHandler(IMapper mapper, IAsyncRepository<Ingredient> ingresientRepository)
        {
            _mapper = mapper;
            _ingredientRepository = ingresientRepository;
        }
        public async Task<List<IngredientDto>> Handle(GetIngredientsListQuery request, CancellationToken cancellationToken)
        {
            var allIngredients = (await _ingredientRepository.ListAllAsync()).OrderBy(x => x.Id);
            return _mapper.Map<List<IngredientDto>>(allIngredients);
        }
    }
}
