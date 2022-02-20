using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Users.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}
