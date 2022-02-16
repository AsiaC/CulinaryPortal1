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

namespace CulinaryPortal.Application.Features.Cookbooks.Queries.GetCookbooksList
{
    public class GetCookbooksListQueryHandler : IRequestHandler<GetCookbooksListQuery, List<CookbookDto>>
    {
        private readonly ICookbookRepository _cookbookRepository;
        private readonly IMapper _mapper;
        public GetCookbooksListQueryHandler(IMapper mapper, ICookbookRepository cookbookRepository)
        {
            _mapper = mapper;
            _cookbookRepository = cookbookRepository;
        }
        public async Task<List<CookbookDto>> Handle(GetCookbooksListQuery request, CancellationToken cancellationToken)
        {
            var allCookbooks = (await _cookbookRepository.GetCookbooksWithRecipesAsync()).OrderBy(x => x.Id);
            return _mapper.Map<List<CookbookDto>>(allCookbooks);
        }
    }
}
