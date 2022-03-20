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

namespace CulinaryPortal.Application.Features.Photos.Commands.UpdatePhoto
{
    public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand>
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;
        public UpdatePhotoCommandHandler(IMapper mapper, IPhotoRepository photoRepository)
        {
            _mapper = mapper;
            _photoRepository = photoRepository;
        }
        public async Task<Unit> Handle(UpdatePhotoCommand request, CancellationToken cancellationToken)
        {
            var objectToUpdate = await _photoRepository.GetByIdAsync(request.Id);

            //if (objectToUpdate == null)
           // {
                //TODO DODAC EXCEPTIONS
                //throw new NotFoundException(nameof(Event), request.EventId);
            //}
            //TODO co z items? validacja?
            _mapper.Map(request,objectToUpdate,typeof(UpdatePhotoCommand),typeof(Photo));

            await _photoRepository.UpdateAsync(objectToUpdate);

            return Unit.Value;
        }
    }
}
