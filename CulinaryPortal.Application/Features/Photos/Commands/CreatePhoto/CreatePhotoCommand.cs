using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Photos.Commands.CreatePhoto
{
    public class CreatePhotoCommand : IRequest<PhotoDto>
    {
        public byte[] ContentPhoto { get; set; }

        public bool IsMain { get; set; }
        public int RecipeId { get; set; }
    }
}
