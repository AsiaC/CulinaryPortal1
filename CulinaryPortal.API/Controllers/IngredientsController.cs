using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CulinaryPortal.API.DbContexts;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Services;
using AutoMapper;
using CulinaryPortal.API.Models;

namespace CulinaryPortal.API.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;

        public IngredientsController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetIngredients()
        {
            try
            {
                var ingredientsFromRepo = await _culinaryPortalRepository.GetIngredientsAsync();
                return Ok(_mapper.Map<IEnumerable<Models.IngredientDto>>(ingredientsFromRepo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        // GET: api/ingredients/5
        [HttpGet("{ingredientId}", Name = "GetIngredient")]
        public async Task<ActionResult<IngredientDto>> GetIngredient([FromRoute] int ingredientId)
        {
            try
            {
                var ingredinetFromRepo = await _culinaryPortalRepository.GetIngredientAsync(ingredientId);
                if (ingredinetFromRepo == null)
                {
                    return NotFound();
                }                
                return Ok(_mapper.Map<IngredientDto>(ingredinetFromRepo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        //api/ingredients
        [HttpPost]
        public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] IngredientDto ingredientDto)
        {
            try
            {
                var ingredient = _mapper.Map<Ingredient>(ingredientDto);
                await _culinaryPortalRepository.AddIngredientAsync(ingredient);

                return CreatedAtAction(nameof(GetIngredient), new { ingredientId = ingredient.Id }, ingredient);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
        //todo to do usuniecia chyba?
        //// DELETE: api/Ingredients/5
        //[HttpDelete("{ingredientId}")]
        //public async Task<ActionResult> DeleteIngredient([FromRoute] int ingredientId)
        //{
        //    try
        //    {
        //        var ingredientFromRepo = await _culinaryPortalRepository.GetIngredientAsync(ingredientId);
        //        if (ingredientFromRepo == null)
        //        {
        //            return NotFound();
        //        }
        //        await _culinaryPortalRepository.DeleteIngredientAsync(ingredientFromRepo);

        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e);
        //    }
        //}
    }
}
