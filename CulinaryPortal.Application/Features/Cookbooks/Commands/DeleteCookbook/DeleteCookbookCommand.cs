using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Cookbooks.Commands.DeleteCookbook
{
    public class DeleteCookbookCommand : IRequest
    {
        public int Id { get; set; }
    }
}
