﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Authorization;
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
        //[AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            var recipesFromRepo = await _culinaryPortalRepository.GetRecipesAsync();
            return Ok(_mapper.Map<IEnumerable<RecipeDto>>(recipesFromRepo));
        }

        // GET: api/recipes/5
        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public async Task<ActionResult<Recipe>> GetRecipe(int recipeId)
        {
            var checkIfRecipeExists = _culinaryPortalRepository.RecipeExists(recipeId);

            if (checkIfRecipeExists == false)
            {
                return NotFound();
            }
            var recipeFromRepo = await _culinaryPortalRepository.GetRecipeAsync(recipeId);
            return Ok(_mapper.Map<RecipeDto>(recipeFromRepo));
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
        public async Task<ActionResult> DeleteRecipe(int recipeId)
        {
            var recipeFromRepo = await _culinaryPortalRepository.GetRecipeAsync(recipeId);
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
