using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Domain.Entities;
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
        private readonly IAsyncRepository<Rate> _rateRepository;

        public DeleteRateCommandHandler(IAsyncRepository<Rate> rateRepository)
        {
            _rateRepository = rateRepository;
        }
        public async Task<Unit> Handle(DeleteRateCommand request, CancellationToken cancellationToken)
        {
            var objectToDelete = await _rateRepository.GetByIdAsync(request.Id);
            if (objectToDelete == null)
            {
                throw new Exception("Server error while removing the rate. Object not found.");
            }
            await _rateRepository.DeleteAsync(objectToDelete);
            return Unit.Value;
        }
    }
}
