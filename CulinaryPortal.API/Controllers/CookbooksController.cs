using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CulinaryPortal.API.Controllers
{
    [Route("api/cookbooks")]
    [ApiController]
    public class CookbooksController : ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;

        public CookbooksController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/cookbooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CookbookDto>>> GetCookbooks()
        {
            try
            {
                var cookbooksFromRepo = await _culinaryPortalRepository.GetCookbooksAsync();
                return Ok(_mapper.Map<IEnumerable<CookbookDto>>(cookbooksFromRepo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        // GET: api/cookbooks/5
        [HttpGet("{cookbookId}", Name = "GetCookbook")]
        public async Task<ActionResult<CookbookDto>> GetCookbook([FromRoute] int cookbookId)
        {
            try
            {
                var cookbookFromRepo = await _culinaryPortalRepository.GetCookbookAsync(cookbookId);
                if (cookbookFromRepo == null)
                {
                    return NotFound();
                }     
                return Ok(_mapper.Map<CookbookDto>(cookbookFromRepo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        //POST: api/cookbooks
        [HttpPost]
        public async Task<ActionResult<Cookbook>> CreateCookbook([FromBody] CookbookDto cookbookDto)
        {
            try
            {
                var cookbook = _mapper.Map<Cookbook>(cookbookDto);
                await _culinaryPortalRepository.AddCookbookAsync(cookbook);
                return CreatedAtAction(nameof(GetCookbook), new { cookbookId = cookbook.Id }, cookbook);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        

        // DELETE: api/cookbooks/5
        [HttpDelete("{cookbookId}")]
        public async Task<ActionResult> DeleteCookbook([FromRoute] int cookbookId)
        {   
            try
            {
                var cookbookFromRepo = await _culinaryPortalRepository.GetCookbookAsync(cookbookId);
                if (cookbookFromRepo == null)
                {
                    return NotFound();
                }
                await _culinaryPortalRepository.DeleteCookbookAsync(cookbookFromRepo);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // PUT: api/cookbooks/5 //TODO TYLKO ZROBIONE NA RemoveRecipeFromCookbook 
        [HttpPut("{cookbookId}")]
        public async Task<ActionResult> UpdateCookbook([FromRoute] int cookbookId, [FromBody] CookbookRecipeDto cookbookRecipeDto)
        {
            if (!ModelState.IsValid) //TODO CZY TO JEST POTRZEBNE?
            {
                return BadRequest(ModelState);
            }
            try
            {//TODO czy user jest potrzebny? 
                var user = await _culinaryPortalRepository.GetUserAsync(cookbookRecipeDto.UserId);
                var cookbook = await _culinaryPortalRepository.GetCookbookAsync(user.Cookbook.Id);

                var recipeToDelete = cookbook.CookbookRecipes.First(c => c.RecipeId == cookbookRecipeDto.RecipeId);
                if (recipeToDelete == null)
                {
                    return NotFound();
                }
                cookbook.CookbookRecipes.Remove(recipeToDelete);
                await _culinaryPortalRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
        // PUT: api/cookbook
        [HttpPut] //czy to jest w dobrym kontrolerze ujednolić z lista zakupów
        public async Task<ActionResult> AddRecipeToCookbook([FromBody] CookbookRecipeDto cookbookRecipeDto)
        {            
            //one user only one cookbook   
            try
            {
                var user = await _culinaryPortalRepository.GetUserAsync(cookbookRecipeDto.UserId);
                var cookbook = await _culinaryPortalRepository.GetCookbookAsync(user.Cookbook.Id); //TODO spr

                var recipeToAdd = new CookbookRecipe()
                {
                    CookbookId = cookbook.Id,
                    RecipeId = cookbookRecipeDto.RecipeId
                };
                //var cookbook0 = await _culinaryPortalRepository.GetCookbookAsync(cookbookId);
                cookbook.CookbookRecipes.Add(recipeToAdd);
                await _culinaryPortalRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
