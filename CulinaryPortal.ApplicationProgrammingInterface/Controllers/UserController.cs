using CulinaryPortal.Application.Features.Users.Commands.DeleteUser;
using CulinaryPortal.Application.Features.Users.Commands.UpdateUser;
using CulinaryPortal.Application.Features.Users.Queries.GetUserCookbook;
using CulinaryPortal.Application.Features.Users.Queries.GetUserDetail;
using CulinaryPortal.Application.Features.Users.Queries.GetUserRate;
using CulinaryPortal.Application.Features.Users.Queries.GetUserRecipes;
using CulinaryPortal.Application.Features.Users.Queries.GetUserShoppingLists;
using CulinaryPortal.Application.Features.Users.Queries.GetUsersList;
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
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "OnlyAdminRole")]
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            try
            {
                var dtos = await _mediator.Send(new GetUsersListQuery());
                return Ok(dtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            try
            {
                var getUserDetailQuery = new GetUserDetailQuery() { Id = userId };
                var recipe = await _mediator.Send(getUserDetailQuery);
                if (recipe == null) //todo nie jestem pewna czy null, czy coś innego bo w base repo jest Find a nie FirstOrDetail
                {
                    return NotFound();
                }
                return Ok(recipe);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        [Authorize(Policy = "OnlyAdminRole")]
        [HttpDelete("{userId}", Name = "DeleteUser")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            try
            {
                var deleteUserCommand = new DeleteUserCommand() { Id = userId };
                await _mediator.Send(deleteUserCommand);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
        {
            try
            {
                await _mediator.Send(updateUserCommand);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        // GET: api/users/3/cookbook
        [HttpGet("{userId}/cookbook", Name = "GetUserCookbook")]
        public async Task<ActionResult<CookbookDto>> GetUserCookbookAsync([FromRoute] int userId)
        {
            try
            {
                var getUserCookbookQuery = new GetUserCookbookQuery() { UserId = userId };
                var cookbook = await _mediator.Send(getUserCookbookQuery);
                if (cookbook == null) //todo spr
                {
                    return NotFound();
                }
                return Ok(cookbook);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // GET: api/users/3/recipes
        [HttpGet("{userId}/recipes", Name = "GetUserRecipes")]
        public async Task<ActionResult<List<RecipeDto>>> GetUserRecipesAsync([FromRoute] int userId)
        {
            try
            {
                var getUserRecipesQuery = new GetUserRecipesQuery() { UserId = userId };
                var recipes = await _mediator.Send(getUserRecipesQuery);
                if (recipes.Any())
                {
                    return Ok(recipes);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // GET: api/users/3/shoppingLists
        [HttpGet("{userId}/shoppingLists", Name = "GetUserShoppingLists")]
        public async Task<ActionResult<List<ShoppingListDto>>> GetUserShoppingListsAsync([FromRoute] int userId)
        {
            try
            {
                var getUserShoppingListsQuery = new GetUserShoppingListsQuery() { UserId = userId };
                var userShoppingLists = await _mediator.Send(getUserShoppingListsQuery);
                if (userShoppingLists.Any())
                {
                    return Ok(userShoppingLists);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // GET: api/users/3/recipes/1
        [HttpGet("{userId}/recipes/{recipeId}", Name = "GetUserRecipeRate")]
        public async Task<ActionResult<RateDto>> GetUserRecipeRate([FromRoute] int userId, [FromRoute] int recipeId)
        {
            try
            {
                var getUserRateQuery = new GetUserRateQuery() { UserId = userId, RecipeId = recipeId };
                var rate = await _mediator.Send(getUserRateQuery);
                if (rate == null)
                {
                    return NotFound();                    
                }
                return Ok(rate);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

    }
}
