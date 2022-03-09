using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Ingredients.Commands.CreateIngredient
{
    public class CreateIngredientCommand : IRequest<IngredientDto>
    {
        public string Name { get; set; }
    }
}
