using CulinaryPortal.Application.Models;
using CulinaryPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Users.Queries.GetUsersList
{
    public class GetUsersListQuery : IRequest<List<UserDto>>
    {
    }
}
