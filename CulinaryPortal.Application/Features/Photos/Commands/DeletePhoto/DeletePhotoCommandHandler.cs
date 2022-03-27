using CulinaryPortal.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Photos.Commands.DeletePhoto
{
    public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand>
    {
        private readonly IPhotoRepository _photoRepository;

        public DeletePhotoCommandHandler(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task<Unit> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
            var objectToDelete = await _photoRepository.GetByIdAsync(request.Id);
            if (objectToDelete == null)
            {
                throw new Exception("Server error while removing the photo. Object not found.");
            }
            await _photoRepository.DeleteAsync(objectToDelete);
            return Unit.Value;
        }
    }
}
