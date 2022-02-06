using CulinaryPortal.Application.Features.Recipes.Queries.GetRecipesList;
using CulinaryPortal.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.ApplicationProgrammingInterface.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IMediator _mediator;

        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize]
        [HttpGet("all", Name = "GetAllRecipes")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<RecipeDto>>> GetAllCategories()
        {
            var dtos = await _mediator.Send(new GetRecipesListQuery());
            return Ok(dtos);
        }

    }
}
