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

namespace CulinaryPortal.Application.Features.Photos.Queries.GetPhotoDetail
{
    public class GetPhotoDetailQueryHandler : IRequestHandler<GetPhotoDetailQuery, PhotoDto>
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;
        public GetPhotoDetailQueryHandler(IMapper mapper, IPhotoRepository photoRepository)
        {
            _mapper = mapper;
            _photoRepository = photoRepository;
        }
        public async Task<PhotoDto> Handle(GetPhotoDetailQuery request, CancellationToken cancellationToken)
        {
            var photo = await _photoRepository.GetByIdAsync(request.Id);
            var photoDto = _mapper.Map<PhotoDto>(photo);
            return photoDto;
        }
    }
}
