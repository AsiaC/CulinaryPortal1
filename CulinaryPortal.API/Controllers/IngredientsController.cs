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
        public ActionResult<IEnumerable<Ingredient>> GetIngredients()
        {
            var ingredientsFromRepo = _culinaryPortalRepository.GetIngredients();
            return Ok(ingredientsFromRepo);
        }

        // GET: api/ingredients/5
        [HttpGet("{ingredientId}", Name = "GetIngredient")]
        public ActionResult<Ingredient> GetIngredient(int ingredientId)
        {
            var checkIfIngredientExists = _culinaryPortalRepository.IngredientExists(ingredientId);

            if (checkIfIngredientExists == null)
            {
                return NotFound();
            }
            var ingredinetFromRepo = _culinaryPortalRepository.GetIngredient(ingredientId);
            return Ok(ingredinetFromRepo);
        }

        [HttpPost]
        public ActionResult<Ingredient> CreateIngredient(Ingredient ingredient)
        {
            _culinaryPortalRepository.AddIngredient(ingredient);
            _culinaryPortalRepository.Save();

            return CreatedAtAction("GetIngredient", new { ingredientId = ingredient.Id }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{ingredientId}")]
        public ActionResult DeleteIngredient(int ingredientId)
        {
            var ingredientFromRepo = _culinaryPortalRepository.GetIngredient(ingredientId);
            if (ingredientFromRepo == null)
            {
                return NotFound();
            }

            _culinaryPortalRepository.DeleteIngredient(ingredientFromRepo);
            _culinaryPortalRepository.Save();

            return NoContent();
        }







        //// PUT: api/Ingredients/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutIngredient(int id, Ingredient ingredient)
        //{
        //    if (id != ingredient.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(ingredient).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!IngredientExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Ingredients
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
        //{
        //    _context.Ingredients.Add(ingredient);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
        //}



    }
}
