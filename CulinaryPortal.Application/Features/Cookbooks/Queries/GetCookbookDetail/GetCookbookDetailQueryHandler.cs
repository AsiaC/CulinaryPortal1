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

namespace CulinaryPortal.Application.Features.Cookbooks.Queries.GetCookbookDetail
{
    public class GetCookbookDetailQueryHandler : IRequestHandler<GetCookbookDetailQuery, CookbookDto>
    {
        private readonly ICookbookRepository _cookbookRepository;
        private readonly IMapper _mapper;
        public GetCookbookDetailQueryHandler(IMapper mapper, ICookbookRepository cookbookRepository)
        {
            _mapper = mapper;
            _cookbookRepository = cookbookRepository;
        }

        public async Task<CookbookDto> Handle(GetCookbookDetailQuery request, CancellationToken cancellationToken)
        {
            var @cookbook = await _cookbookRepository.GetCookbookWithRecipesAsync(request.Id);
            var cookbookDetailDto = _mapper.Map<CookbookDto>(@cookbook);
            return cookbookDetailDto;
        }
    }
}
