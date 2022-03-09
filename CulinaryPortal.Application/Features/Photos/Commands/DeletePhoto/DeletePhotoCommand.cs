using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Photos.Commands.DeletePhoto
{
    public class DeletePhotoCommand : IRequest
    {
        public int Id { get; set; }
    }
}
