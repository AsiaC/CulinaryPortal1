using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Rates.Queries.GetRatesList
{
    public class GetRatesListQuery : IRequest<List<RateDto>>
    {
    }
}
