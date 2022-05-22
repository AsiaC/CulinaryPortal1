using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Rates.Queries.GetRateDetail
{
    public class GetRateDetailQuery : IRequest<RateDto>
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
    }
}
