using CulinaryPortal.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var objectToDelete = await _userRepository.GetByIdAsync(request.Id);
            if (objectToDelete == null)
            {
                throw new Exception("Server error while removing the user. Object not found.");
            }
            await _userRepository.DeleteAsync(objectToDelete);
            return Unit.Value;
        }
    }
}
