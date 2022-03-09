using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Photos.Commands.UpdatePhoto
{
    public class UpdatePhotoCommand : IRequest
    {
        public int Id { get; set; }

        public byte[] ContentPhoto { get; set; }

        public bool IsMain { get; set; }
    }
}
