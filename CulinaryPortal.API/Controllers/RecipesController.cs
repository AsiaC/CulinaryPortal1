using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CulinaryPortal.API.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;

        public RecipesController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/recipes
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetRecipes()
        {
            var recipesFromRepo = _culinaryPortalRepository.GetRecipes();
            return Ok(recipesFromRepo);
        }

        // GET: api/recipes/5
        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public ActionResult<Recipe> GetRecipe(int recipeId)
        {
            var checkIfRecipeExists = _culinaryPortalRepository.RecipeExists(recipeId);

            if (checkIfRecipeExists == null)
            {
                return NotFound();
            }
            var ingredinetFromRepo = _culinaryPortalRepository.GetRecipe(recipeId);
            return Ok(ingredinetFromRepo);
        }

        [HttpPost]
        public ActionResult<Recipe> CreateRecipe(Recipe recipe)
        {
            _culinaryPortalRepository.AddRecipe(recipe);
            _culinaryPortalRepository.Save();

            return CreatedAtAction("GetRecipe", new { recipeId = recipe.Id }, recipe);
        }

        // DELETE: api/recipes/5
        [HttpDelete("{recipeId}")]
        public ActionResult DeleteRecipe(int recipeId)
        {
            var recipeFromRepo = _culinaryPortalRepository.GetRecipe(recipeId);
            if (recipeFromRepo == null)
            {
                return NotFound();
            }

            _culinaryPortalRepository.DeleteRecipe(recipeFromRepo);
            _culinaryPortalRepository.Save();

            return NoContent();
        }
    }
}
