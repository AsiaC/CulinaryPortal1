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

namespace CulinaryPortal.Application.Features.Users.Queries.GetUserShoppingLists
{
    public class GetUserShoppingListsQueryHandler : IRequestHandler<GetUserShoppingListsQuery, List<ShoppingListDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserShoppingListsQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<List<ShoppingListDto>> Handle(GetUserShoppingListsQuery request, CancellationToken cancellationToken)
        {
            var userShoppingLists = await _userRepository.GetUserShoppingListsAsync(request.UserId);
            var userShoppingListsDto = _mapper.Map<List<ShoppingListDto>>(userShoppingLists);
            return userShoppingListsDto;
        }
    }
}
