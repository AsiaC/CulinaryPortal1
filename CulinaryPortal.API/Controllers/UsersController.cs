using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;

        public UsersController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [Authorize(Policy = "OnlyAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var usersFromRepo = await _culinaryPortalRepository.GetUsersAsync();
                return Ok(_mapper.Map<IEnumerable<UserDto>>(usersFromRepo));
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
                var userFromRepo = await _culinaryPortalRepository.GetUserAsync(userId);
                if (userFromRepo == null)
                {
                    return NotFound();
                }                
                return Ok(_mapper.Map<UserDto>(userFromRepo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }           
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] User user)
        {
            try
            {
                await _culinaryPortalRepository.AddUserAsync(user);
                var userToReturn = _mapper.Map<UserDto>(user);
                return CreatedAtAction(nameof(GetUser), new { userId = userToReturn.Id }, userToReturn);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }           
        }

        [Authorize(Policy = "OnlyAdminRole")]
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int userId)
        {
            try
            {
                var userFromRepo = await _culinaryPortalRepository.GetUserAsync(userId);
                if (userFromRepo == null)
                {
                    return NotFound();
                }
                await _culinaryPortalRepository.DeleteUserAsync(userFromRepo);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
                
        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _culinaryPortalRepository.GetUserAsync(userUpdateDto.Id);
                if (user == null)
                {
                    return NotFound();
                }
                _mapper.Map(userUpdateDto, user);

                //await _culinaryPortalRepository.UpdateUserAsync(user);

                if (await _culinaryPortalRepository.SaveAllAsync()) 
                    return NoContent();
                return BadRequest("Failed to update user");
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
                var cookbookFromRepo = await _culinaryPortalRepository.GetUserCookbookAsync(userId);
                if (cookbookFromRepo == null) //==false? spr co zwróci?
                {
                    return NotFound();
                }
                var cookbook = _mapper.Map<CookbookDto>(cookbookFromRepo);

                return Ok(cookbook);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // GET: api/users/3/recipes
        [HttpGet("{userId}/recipes", Name = "GetUserRecipes")]
        public async Task<IActionResult> GetUserRecipesAsync([FromRoute] int userId)
        {
            try
            {
                var recipesFromRepo = await _culinaryPortalRepository.GetUserRecipesAsync(userId);
                if (recipesFromRepo.Any())
                {
                    return Ok(_mapper.Map<IEnumerable<RecipeDto>>(recipesFromRepo));
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
        public async Task<IActionResult> GetUserShoppingListsAsync([FromRoute] int userId)
        {
            try
            {
                var shoppingListsFromRepo = await _culinaryPortalRepository.GetUserShoppingListsAsync(userId);
                if (shoppingListsFromRepo == null || shoppingListsFromRepo.Count() == 0)
                {
                    return NotFound();
                }

                var userShoppingLists = _mapper.Map<IEnumerable<ShoppingListDto>>(shoppingListsFromRepo);
                return Ok(userShoppingLists);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // GET: api/users/3/recipes/1
        [HttpGet("{userId}/recipes/{recipeId}", Name = "GetUserRecipeRate")]
        public async Task<ActionResult<Rate>> GetUserRecipeRate([FromRoute] int userId, [FromRoute] int recipeId)
        {
            try
            {  
                var rateFromRepo = await _culinaryPortalRepository.GetUserRecipeRateAsync(userId, recipeId); ;
                if (rateFromRepo == null)
                {
                    return NotFound();
                }
                var rate = _mapper.Map<RateDto>(rateFromRepo);

                return Ok(rate);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        //// PUT: api/users/3/recipes/search
        [HttpPut("{userId}/recipes/search")]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> SearchUserRecipesAsync([FromRoute] int userId, [FromBody] SearchRecipeDto searchRecipeDto)
        {
            try
            {
                var recipesFromRepo = await _culinaryPortalRepository.SearchRecipesAsync(searchRecipeDto);
                if (recipesFromRepo.Any())
                {
                    return Ok(_mapper.Map<IEnumerable<RecipeDto>>(recipesFromRepo));
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        //// PUT: api/users/3/cookbook/search
        [HttpPut("{userId}/cookbook/search")]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> SearchUserCookbookAsync([FromRoute] int userId, [FromBody] SearchRecipeDto searchRecipeDto)
        {
            try
            {
                var cookbookRecipesFromRepo = await _culinaryPortalRepository.SearchUserCookbookRecipesAsync(searchRecipeDto);
                if (cookbookRecipesFromRepo.Any())
                {
                    return Ok(_mapper.Map<IEnumerable<RecipeDto>>(cookbookRecipesFromRepo));
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

        }
      
    }
}
