using CulinaryPortal.Application.Features.Recipes.Commands.DeleteRecipe;
using CulinaryPortal.Application.Features.Recipes.Queries.GetRecipeDetail;
using CulinaryPortal.Application.Features.Recipes.Queries.GetRecipesList;
using CulinaryPortal.Application.Models;
using CulinaryPortal.Domain.Entities;
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
    public class RecipeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize]
        [HttpGet(Name = "GetRecipes")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<RecipeDto>>> GetRecipes()
        {
            var dtos = await _mediator.Send(new GetRecipesListQuery());
            return Ok(dtos);
        }

        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(int recipeId)
        { //todo czy try catch tu potrzeba?
            var getRecipeDetailQuery = new GetRecipeDetailQuery() { Id = recipeId };
            var recipe = await _mediator.Send(getRecipeDetailQuery);
            if (recipe == null) //todo nie jestem pewna czy null, czy coś innego bo w base repo jest Find a nie FirstOrDetail
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost(Name = "AddRecipe")] //CreateRecipe
        public async Task<ActionResult<Recipe>> Create([FromBody] RecipeDto createRecipeDtoCommand)
        {//todo dodac handler
            var response = await _mediator.Send(createRecipeDtoCommand); 
            return Ok(response);
        }

        [HttpDelete("{recipeId}", Name = "DeleteRecipe")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int recipeId) //DeleteRecipe
        {
            var deleteRecipeCommand = new DeleteRecipeCommand() { RecipeId = recipeId };
            await _mediator.Send(deleteRecipeCommand);
            return NoContent();
        }

        [HttpPut(Name = "UpdateRecipe")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] RecipeDto updateRecipeCommand)
        { //todo dokoncz
            await _mediator.Send(updateRecipeCommand);
            return NoContent();
        }

    }
}
