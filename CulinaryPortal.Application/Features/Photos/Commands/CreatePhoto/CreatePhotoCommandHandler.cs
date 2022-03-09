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

namespace CulinaryPortal.Application.Features.Photos.Commands.CreatePhoto
{
    public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, PhotoDto>
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;

        public CreatePhotoCommandHandler(IMapper mapper, IPhotoRepository photoRepository)
        {
            _mapper = mapper;
            _photoRepository = photoRepository;
        }

        public async Task<PhotoDto> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
        {
            var @event = _mapper.Map<Photo>(request);

            @event = await _photoRepository.AddAsync(@event);

            var objestToReturn = _mapper.Map<PhotoDto>(@event);

            return objestToReturn;
        }
    }
}
