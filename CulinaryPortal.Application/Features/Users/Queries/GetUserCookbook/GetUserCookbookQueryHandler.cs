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

namespace CulinaryPortal.Application.Features.Users.Queries.GetUserCookbook
{
    public class GetUserCookbookQueryHandler : IRequestHandler<GetUserCookbookQuery, CookbookDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserCookbookQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<CookbookDto> Handle(GetUserCookbookQuery request, CancellationToken cancellationToken)
        {
            var userCookbook = await _userRepository.GetUserCookbookAsync(request.UserId);            
            var cookbookDto = _mapper.Map<CookbookDto>(userCookbook);
            return cookbookDto;
        }
    }
}
