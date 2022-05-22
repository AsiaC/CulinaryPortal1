using CulinaryPortal.Application.Features.Cookbooks.Queries.GetCookbookDetail;
using CulinaryPortal.Application.Features.Cookbooks.Queries.GetCookbookRecipes;
using CulinaryPortal.Application.Features.Rates.Queries.GetRateDetail;
using CulinaryPortal.Application.Features.Recipes.Queries.GetRecipesList;
using CulinaryPortal.Application.Features.ShoppingLists.Queries.GetShoppingListsList;
using CulinaryPortal.Application.Features.Users.Commands.DeleteUser;
using CulinaryPortal.Application.Features.Users.Commands.UpdateUser;
using CulinaryPortal.Application.Features.Users.Queries.GetUserDetail;
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

        [Authorize(Roles = "Admin")]
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
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] int userId)
        {
            try
            {
                var getUserDetailQuery = new GetUserDetailQuery() { Id = userId };
                var recipe = await _mediator.Send(getUserDetailQuery);
                if (recipe == null)
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}", Name = "DeleteUser")]
        public async Task<ActionResult> DeleteUser([FromRoute] int userId)
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
        [Authorize(Roles = "Member")]
        [HttpGet("{userId}/cookbook", Name = "GetUserCookbook")]
        public async Task<ActionResult<CookbookDto>> GetUserCookbookAsync([FromRoute] int userId)
        {
            try
            {
                var getUserCookbookQuery = new GetCookbookDetailQuery() { UserId = userId };
                var cookbook = await _mediator.Send(getUserCookbookQuery);
                if (cookbook == null)
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
        [Authorize(Roles = "Member")]
        [HttpGet("{userId}/recipes", Name = "GetUserRecipes")]
        public async Task<ActionResult<List<RecipeDto>>> GetUserRecipesAsync([FromRoute] int userId)
        {
            try
            {
                var getUserRecipesQuery = new GetRecipesListQuery() { UserId = userId };
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
        [Authorize(Roles = "Member")]
        [HttpGet("{userId}/shoppingLists", Name = "GetUserShoppingLists")]
        public async Task<ActionResult<List<ShoppingListDto>>> GetUserShoppingListsAsync([FromRoute] int userId)
        {
            try
            {
                var getUserShoppingListsQuery = new GetShoppingListsListQuery() { UserId = userId };
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
        [Authorize(Roles = "Member")]
        [HttpGet("{userId}/recipes/{recipeId}", Name = "GetUserRecipeRate")]
        public async Task<ActionResult<RateDto>> GetUserRecipeRate([FromRoute] int userId, [FromRoute] int recipeId)
        {
            try
            {
                var getUserRateQuery = new GetRateDetailQuery() { UserId = userId, RecipeId = recipeId };
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

        // GET: api/users/3/recipes/search
        [Authorize(Roles = "Member")]
        [HttpGet("{userId}/recipes/search", Name = "SearchUserRecipes")]
        public async Task<ActionResult<List<RecipeDto>>> SearchUserRecipes(string name, int? categoryId, int? difficultyLevelId, int? preparationTimeId, int? userId)
        {
            try
            {
                var searchUserRecipesQuery = new GetRecipesListQuery() { Name = name, CategoryId = categoryId, DifficultyLevelId = difficultyLevelId, PreparationTimeId = preparationTimeId, UserId = userId};
                var dtos = await _mediator.Send(searchUserRecipesQuery);
                return Ok(dtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // GET: api/users/3/cookbook/search
        [Authorize(Roles = "Member")]
        [HttpGet("{userId}/cookbook/search", Name = "SearchUserCookbookRecipes")]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> SearchUserCookbookRecipes(string name, int? categoryId, int? difficultyLevelId, int? preparationTimeId, int? userId)
        {
            try
            {
                var searchUserCookbookRecipesQuery = new GetCookbookRecipesQuery() { Name = name, CategoryId = categoryId, DifficultyLevelId = difficultyLevelId, PreparationTimeId = preparationTimeId, UserId = userId};
                var dtos = await _mediator.Send(searchUserCookbookRecipesQuery);
                return Ok(dtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
