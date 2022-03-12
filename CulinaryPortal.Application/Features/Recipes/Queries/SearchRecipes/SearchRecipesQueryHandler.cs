using AutoMapper;
using CulinaryPortal.Application.Models;
using CulinaryPortal.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Recipes.Queries.SearchRecipes
{
    public class SearchRecipesQueryHandler : IRequestHandler<SearchRecipesQuery, List<RecipeDto>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        public SearchRecipesQueryHandler(IMapper mapper, IRecipeRepository recipeRepository)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }
        public async Task<List<RecipeDto>> Handle(SearchRecipesQuery request, CancellationToken cancellationToken)
        {
            var list = await _recipeRepository.SearchRecipesAsync(request.Name, request.CategoryId, request.DifficultyLevelId, request.PreparationTimeId, request.UserId, request.Top);
            var searchRecipesDto = _mapper.Map<List<RecipeDto>>(list);

            if (request.Top != null)
            {
                searchRecipesDto = searchRecipesDto.OrderByDescending(r => r.TotalScore).ThenBy(x => x.Name).Take(request.Top ?? 0).ToList();
            }

            return searchRecipesDto;
        }
    }
}
