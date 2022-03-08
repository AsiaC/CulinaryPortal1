using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Users.Queries.GetUserRate
{
    public class GetUserRateQuery : IRequest<RateDto>
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
    }
}
