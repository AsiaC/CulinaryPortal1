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

namespace CulinaryPortal.Application.Features.Users.Queries.GetUserRecipes
{
    public class GetUserRecipesQueryHandler : IRequestHandler<GetUserRecipesQuery, List<RecipeDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserRecipesQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<List<RecipeDto>> Handle(GetUserRecipesQuery request, CancellationToken cancellationToken)
        {
            var userRecipes = await _userRepository.GetUserRecipesAsync(request.UserId);
            var userRecipesDto = _mapper.Map<List<RecipeDto>>(userRecipes);
            return userRecipesDto;
        }
    }
}
