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

namespace CulinaryPortal.Application.Features.Cookbooks.Queries.GetCookbookRecipes
{
    public class GetCookbookRecipesQueryHandler : IRequestHandler<GetCookbookRecipesQuery, List<RecipeDto>>
    {
        private readonly ICookbookRepository _cookbookRepository;
        private readonly IMapper _mapper;
        public GetCookbookRecipesQueryHandler(IMapper mapper, ICookbookRepository cookbookRepository)
        {
            _mapper = mapper;
            _cookbookRepository = cookbookRepository;
        }
        public async Task<List<RecipeDto>> Handle(GetCookbookRecipesQuery request, CancellationToken cancellationToken)
        {
            var list = await _cookbookRepository.SearchCookbookUserRecipesAsync(request.Name, request.CategoryId, request.DifficultyLevelId, request.PreparationTimeId, request.UserId);
            return _mapper.Map<List<RecipeDto>>(list);
        }
    }
}
