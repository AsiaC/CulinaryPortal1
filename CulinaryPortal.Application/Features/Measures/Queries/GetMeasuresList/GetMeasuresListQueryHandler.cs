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

namespace CulinaryPortal.Application.Features.Measures.Queries.GetMeasuresList
{
    public class GetMeasuresListQueryHandler : IRequestHandler<GetMeasuresListQuery, List<MeasureDto>>
    {
        private readonly IAsyncRepository<Measure> _measureRepository;
        private readonly IMapper _mapper;
        public GetMeasuresListQueryHandler(IMapper mapper, IAsyncRepository<Measure> measureRepository)
        {
            _mapper = mapper;
            _measureRepository = measureRepository;
        }
        public async Task<List<MeasureDto>> Handle(GetMeasuresListQuery request, CancellationToken cancellationToken)
        {           
            var allMeasures = (await _measureRepository.ListAllAsync()).OrderBy(x => x.Id);
            return _mapper.Map<List<MeasureDto>>(allMeasures);
        }
    }
}
