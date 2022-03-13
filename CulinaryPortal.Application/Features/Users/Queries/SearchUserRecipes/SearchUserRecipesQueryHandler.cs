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

namespace CulinaryPortal.Application.Features.Users.Queries.SearchUserRecipes
{
    public class SearchUserRecipesQueryHandler : IRequestHandler<SearchUserRecipesQuery, List<RecipeDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public SearchUserRecipesQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<RecipeDto>> Handle(SearchUserRecipesQuery request, CancellationToken cancellationToken)
        {
            var list = await _userRepository.SearchUserRecipesAsync(request.Name, request.CategoryId, request.DifficultyLevelId, request.PreparationTimeId, request.UserId);
            return _mapper.Map<List<RecipeDto>>(list);
        }
    }
}
