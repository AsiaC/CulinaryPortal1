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

namespace CulinaryPortal.Application.Features.Photos.Queries.GetPhotosList
{
    public class GetPhotosListQueryHandler : IRequestHandler<GetPhotosListQuery, List<PhotoDto>>
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;
        public GetPhotosListQueryHandler(IMapper mapper, IPhotoRepository photoRepository)
        {
            _mapper = mapper;
            _photoRepository = photoRepository;
        }
        public async Task<List<PhotoDto>> Handle(GetPhotosListQuery request, CancellationToken cancellationToken)
        {
            var recipePhotos = await _photoRepository.GetRecipePhotosAsync(request.RecipeId);
            var recipePhotosDto = _mapper.Map<List<PhotoDto>>(recipePhotos);
            return recipePhotosDto;
        }        
    }
}
