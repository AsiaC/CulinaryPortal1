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

namespace CulinaryPortal.Application.Features.Recipes.Queries.GetRecipeDetail
{
    public class GetRecipeDetailQueryHandler : IRequestHandler<GetRecipeDetailQuery, RecipeDto>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICookbookRepository _cookbookRepository;
        private readonly IMapper _mapper;
        public GetRecipeDetailQueryHandler(IMapper mapper, IRecipeRepository recipeRepository, ICookbookRepository cookbookRepository)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _cookbookRepository = cookbookRepository;
        }
        public async Task<RecipeDto> Handle(GetRecipeDetailQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository.GetRecipeWithDetailsAsync(request.Id);
            var recipeDetailDto = _mapper.Map<RecipeDto>(recipe);
            recipeDetailDto.CountCookbooks = await _cookbookRepository.CountAssociatedCookbooksAsync(request.Id);
            return recipeDetailDto;
        }
    }
}
