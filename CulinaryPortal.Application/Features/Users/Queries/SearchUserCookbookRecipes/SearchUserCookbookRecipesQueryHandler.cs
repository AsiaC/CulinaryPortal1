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

namespace CulinaryPortal.Application.Features.Users.Queries.SearchUserCookbookRecipes
{
    public class SearchUserCookbookRecipesQueryHandler : IRequestHandler<SearchUserCookbookRecipesQuery, List<RecipeDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public SearchUserCookbookRecipesQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<RecipeDto>> Handle(SearchUserCookbookRecipesQuery request, CancellationToken cancellationToken)
        {
            var list = await _userRepository.SearchCookbookUserRecipesAsync(request.Name, request.CategoryId, request.DifficultyLevelId, request.PreparationTimeId, request.UserId);
            return _mapper.Map<List<RecipeDto>>(list);
        }
    }
}
