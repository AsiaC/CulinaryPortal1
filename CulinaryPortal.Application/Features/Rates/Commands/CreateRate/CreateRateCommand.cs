using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Rates.Commands.CreateRate
{
    public class CreateRateCommand : IRequest<RateDto>
    {
        public int Value { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
    }
}
