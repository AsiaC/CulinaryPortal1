using AutoMapper;
using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetCategoriesListQueryHandler(IMapper mapper, ICategoryRepository categoryRepoisitory)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepoisitory;
        }
        public async Task<List<Category>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {            
            var allCategories = (await _categoryRepository.GetCategoriesWithRecipesAsync()).OrderBy(x => x.Id);
            return _mapper.Map<List<Category>>(allCategories);
        }
    }
}
