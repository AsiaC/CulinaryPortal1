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

namespace CulinaryPortal.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var objectToUpdate = await _userRepository.GetByIdAsync(request.Id);

            if (objectToUpdate == null)
            {
                //TODO DODAC EXCEPTIONS
                //throw new NotFoundException(nameof(Event), request.EventId);
            }
            //TODO co z items? validacja?
            var @event = _mapper.Map<User>(request);

            await _userRepository.UpdateAsync(@event);

            return Unit.Value;
        }
    }
}
