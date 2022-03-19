using CulinaryPortal.Application.Features.Ingredients.Commands.CreateIngredient;
using CulinaryPortal.Application.Features.Ingredients.Queries.GetIngredientsList;
using CulinaryPortal.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.ApplicationProgrammingInterface.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    [Authorize]
    public class IngredientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<IngredientDto>>> GetIngredients()
        {
            try
            {
                GetIngredientsListQuery getIngredientsListQuery = new GetIngredientsListQuery();
                var ingredients = await _mediator.Send(getIngredientsListQuery);
                return Ok(ingredients);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }        

        [HttpPost]
        public async Task<ActionResult<IngredientDto>> CreateIngredient([FromBody] CreateIngredientCommand createIngredientCommand)
        {
            try
            {
                var objectToReturn = await _mediator.Send(createIngredientCommand);
                return Ok(objectToReturn);//todo czy musze dodawac metode GetIngredient
                            //return CreatedAtAction(nameof(GetIngredient), objectToReturn);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }
    }
}
