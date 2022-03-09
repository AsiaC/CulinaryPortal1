using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Photos.Queries.GetPhotoDetail
{
    public class GetPhotoDetailQuery : IRequest<PhotoDto>
    {
        public int Id { get; set; }
    }
}
