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

namespace CulinaryPortal.Application.Features.Recipes.Queries.GetRecipePhotos
{
    public class GetRecipePhotosQueryHandler : IRequestHandler<GetRecipePhotosQuery, List<PhotoDto>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        public GetRecipePhotosQueryHandler(IMapper mapper, IRecipeRepository recipeRepository)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }
        public async Task<List<PhotoDto>> Handle(GetRecipePhotosQuery request, CancellationToken cancellationToken)
        {
            var recipePhotos = await _recipeRepository.GetRecipePhotosAsync(request.RecipeId);
            var recipePhotosDto = _mapper.Map<List<PhotoDto>>(recipePhotos);
            return recipePhotosDto;
        }
    }
}
