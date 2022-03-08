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

namespace CulinaryPortal.Application.Features.Users.Queries.GetUserRate
{
    public class GetUserRateQueryHandler : IRequestHandler<GetUserRateQuery, RateDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserRateQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<RateDto> Handle(GetUserRateQuery request, CancellationToken cancellationToken)
        {
            var userRate = await _userRepository.GetUserRecipeRateAsync(request.UserId, request.RecipeId);
            var rateDto = _mapper.Map<RateDto>(userRate);
            return rateDto;
        }
    }
}
