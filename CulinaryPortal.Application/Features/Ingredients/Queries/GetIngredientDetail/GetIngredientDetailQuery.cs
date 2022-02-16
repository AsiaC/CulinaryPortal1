using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Ingredients.Queries.GetIngredientDetail
{
    public class GetIngredientDetailQuery : IRequest<IngredientDto>
    {
        public int Id { get; set; }
    }
}
