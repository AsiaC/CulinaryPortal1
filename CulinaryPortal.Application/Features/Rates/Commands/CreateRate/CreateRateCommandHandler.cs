

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

namespace CulinaryPortal.Application.Features.Rates.Commands.CreateRate
{
    public class CreateRateCommandHandler : IRequestHandler<CreateRateCommand, RateDto>
    {
        private readonly IAsyncRepository<Rate> _rateRepository;
        private readonly IMapper _mapper;

        public CreateRateCommandHandler(IMapper mapper, IAsyncRepository<Rate> rateRepository)
        {
            _mapper = mapper;
            _rateRepository = rateRepository;
        }
        public async Task<RateDto> Handle(CreateRateCommand request, CancellationToken cancellationToken)
        {
            var rate = _mapper.Map<Rate>(request);

            rate = await _rateRepository.AddAsync(rate);

            var objectToReturn = _mapper.Map<RateDto>(rate);

            return objectToReturn;
        }
    }
}
