﻿using CulinaryPortal.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Rates.Commands.DeleteRate
{
    public class DeleteRateCommandHandler : IRequestHandler<DeleteRateCommand>
    {
        private readonly IRateRepository _rateRepository;

        public DeleteRateCommandHandler(IRateRepository rateRepository)
        {            
            _rateRepository = rateRepository;
        }
        public async Task<Unit> Handle(DeleteRateCommand request, CancellationToken cancellationToken)
        {
            var objectToDelete = await _rateRepository.GetByIdAsync(request.Id);

            if (objectToDelete == null)
            {
                //TODO exceptions
                //throw new NotFoundException(nameof(Event), request.EventId);
            }

            await _rateRepository.DeleteAsync(objectToDelete);

            return Unit.Value;
        }
    }
}