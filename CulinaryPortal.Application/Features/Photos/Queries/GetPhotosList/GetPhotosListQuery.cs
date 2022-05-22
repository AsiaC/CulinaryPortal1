using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Photos.Queries.GetPhotosList
{
    public class GetPhotosListQuery : IRequest<List<PhotoDto>>
    {
        public int RecipeId { get; set; }
    }
}
