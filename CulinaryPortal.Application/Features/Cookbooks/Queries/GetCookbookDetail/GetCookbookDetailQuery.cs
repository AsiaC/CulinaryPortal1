using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Cookbooks.Queries.GetCookbookDetail
{
    public class GetCookbookDetailQuery : IRequest<CookbookDto>
    {
        public int Id { get; set; }
    }
}
