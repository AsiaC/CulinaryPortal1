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

namespace CulinaryPortal.Application.Features.Rates.Queries.GetRatesList
{
    public class GetRatesListQueryHandler : IRequestHandler<GetRatesListQuery, List<RateDto>>
    {
        private readonly IRateRepository _rateRepository;
        private readonly IMapper _mapper;

        public GetRatesListQueryHandler(IMapper mapper, IRateRepository rateRepository)
        {
            _mapper = mapper;
            _rateRepository = rateRepository;
        }        

        public async Task<List<RateDto>> Handle(GetRatesListQuery request, CancellationToken cancellationToken)
        {
            var allRates = (await _rateRepository.ListAllAsync()).OrderBy(x => x.Id);
            return _mapper.Map<List<RateDto>>(allRates);
        }
    }
}
