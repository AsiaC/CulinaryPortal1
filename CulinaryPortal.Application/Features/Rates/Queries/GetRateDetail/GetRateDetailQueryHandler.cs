﻿using AutoMapper;
using CulinaryPortal.Application.Models;
using CulinaryPortal.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Rates.Queries.GetRateDetail
{
    public class GetRateDetailQueryHandler : IRequestHandler<GetRateDetailQuery, RateDto>
    {
        private readonly IRateRepository _rateRepository;
        private readonly IMapper _mapper;
        public GetRateDetailQueryHandler(IMapper mapper, IRateRepository rateRepository)
        {
            _mapper = mapper;
            _rateRepository = rateRepository;
        }
        public async Task<RateDto> Handle(GetRateDetailQuery request, CancellationToken cancellationToken)
        {
            var userRate = await _rateRepository.GetRecipeRateAsync(request.UserId, request.RecipeId);
            var rateDto = _mapper.Map<RateDto>(userRate);
            return rateDto;
        }
    }
}
