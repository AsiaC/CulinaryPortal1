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

namespace CulinaryPortal.Application.Features.Recipes.Queries.GetRecipesList
{
    public class GetRecipesListQueryHandler : IRequestHandler<GetRecipesListQuery, List<RecipeDto>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        public GetRecipesListQueryHandler(IMapper mapper, IRecipeRepository recipeRepository)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }
        public async Task<List<RecipeDto>> Handle(GetRecipesListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allRecipes = (await _recipeRepository.GetRecipesWithDetailsAsync()).OrderBy(x => x.Id);
                return _mapper.Map<List<RecipeDto>>(allRecipes);
            }
            catch (Exception e )
            {

                throw;
            }
            
        }
    }
}
