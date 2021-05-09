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
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes()
        {
            var recipesFromRepo = await _culinaryPortalRepository.GetRecipesAsync();
            return Ok(_mapper.Map<IEnumerable<RecipeDto>>(recipesFromRepo));
        }

        // GET: api/recipes/5
        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(int recipeId)
        {
            var checkIfRecipeExists = await _culinaryPortalRepository.RecipeExistsAsync(recipeId);

            if (checkIfRecipeExists == false)
            {
                return NotFound();
            }
            var recipeFromRepo = await _culinaryPortalRepository.GetRecipeAsync(recipeId);
            var recipe = _mapper.Map<RecipeDto>(recipeFromRepo);
             return Ok(recipe);
        }

        [HttpPost]
        public ActionResult<Recipe> CreateRecipe(Recipe recipe)
        {
            _culinaryPortalRepository.AddRecipe(recipe);

            //var i = 0;
            //foreach (var instruction in recipe.Instructions)
            //{
            //    i+= 1;
            //    if (instruction.Step == null || instruction.Step == 0)
            //    {
            //        instruction.Step = i;
            //    }
            //    _culinaryPortalRepository.AddInstruction(instruction);
            //    i++;
            //}

            //foreach (var recipeIngredient in recipe.RecipeIngredients)
            //{
                
            //    recipe.RecipeIngredients.Add(recipeIngredient);
            //}
            //foreach (var recipeIngredient in recipe.RecipeIngredients)
            //{
            //    _culinaryPortalRepository.AddRecipeIngredient(recipeIngredient);
            //}
                      

            _culinaryPortalRepository.SaveChangesAsync();

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

        // PUT: api/recipes/5
        [HttpPut("{recipeId}")]
        public async Task<IActionResult> UpdateRecipe([FromRoute] int recipeId, [FromBody] RecipeDto recipeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (recipeId != recipeDto.Id)
            {
                return BadRequest();
            }
            try
            { 
                var checkIfRecipeExists = await _culinaryPortalRepository.RecipeExistsAsync(recipeId);
                if (checkIfRecipeExists == false)
                {
                    return NotFound();
                }
                var existingRecipe = await _culinaryPortalRepository.GetRecipeAsync(recipeId);
                //var recipe = _mapper.Map<Recipe>(recipeDto);

                existingRecipe.Name = recipeDto.Name;
                existingRecipe.Description = recipeDto.Description;
                //existingRecipe.DifficultyLevel = recipeDto.DifficultyLevel;
                //existingRecipe.PreparationTime = recipeDto.PreparationTime;
                //existingRecipe.Category = recipeDto.Category;
                //Rate, photos
                List<Instruction> copyExInstructions = new List<Instruction>();
                copyExInstructions.AddRange(existingRecipe.Instructions);
                //Instructions
                foreach (var exInstruction in copyExInstructions)
                {
                    var checkIfInstructionExist = recipeDto.Instructions.Any(i => i.Id == exInstruction.Id);
                    if (checkIfInstructionExist)
                    {
                        //check in something was changed
                        //if (exInstruction.Description != )
                        //{
                        //    //_culinaryPortalRepository.UpdateInstruction(exInstruction);
                        //}

                        var instructionDto = recipeDto.Instructions.FirstOrDefault(i => i.Id == exInstruction.Id);

                        exInstruction.Name = instructionDto.Name;
                        exInstruction.Description = instructionDto.Description;
                    }
                    else
                    {
                        existingRecipe.Instructions.Remove(exInstruction);
                        //_culinaryPortalRepository.DeleteInstruction(exInstruction);   
                    }

                }
                var existingInstructionIds = existingRecipe.Instructions.Select(i => i.Id);
                var newInstructions = recipeDto.Instructions.Where(i => !existingInstructionIds.Contains(i.Id));
                foreach (var newInstruction in newInstructions)
                {//todo moze mozna uzywać mapera?
                    var newToAdd = new Instruction() 
                    {
                        Name = newInstruction.Name,
                        Description = newInstruction.Description
                    };

                    existingRecipe.Instructions.Add(newToAdd);                       
                   
                    //_culinaryPortalRepository.AddInstruction(newInstruction);
                }


                //RecipeIngredients
                //delete all existing (related to recipe)
                List<RecipeIngredient> copyExRecipeIngredients = new List<RecipeIngredient>();
                copyExRecipeIngredients.AddRange(existingRecipe.RecipeIngredients);
                foreach (var exRecipeIngredient in copyExRecipeIngredients)
                {
                    existingRecipe.RecipeIngredients.Remove(exRecipeIngredient);
                }
                foreach (var newRecipeIngredient in recipeDto.RecipeIngredients)
                {
                    //var newMeasure = _mapper.Map<Measure>(newRecipeIngredient.Measure);
                    //todo moze mozna uzywać mapera?
                    var exMeasure = await _culinaryPortalRepository.GetMeasureAsync(newRecipeIngredient.Measure.Id);

                    var newToAdd = new RecipeIngredient()
                    {
                        Quantity = newRecipeIngredient.Quantity,
                        MeasureId = newRecipeIngredient.Measure.Id,
                        IngredientId = newRecipeIngredient.Ingredient.Id,
                        //Measure = _mapper.Map<Measure>(newRecipeIngredient.Measure),
                        //Ingredient = _mapper.Map<Ingredient>(newRecipeIngredient.Ingredient),
                        Measure = exMeasure,
                        RecipeId = recipeId
                    };
                    existingRecipe.RecipeIngredients.Add(newToAdd);
                }


                //existingRecipe.RecipeIngredients.Clear();
                //foreach (var exRecipeIngredient in existingRecipe.RecipeIngredients)
                //{
                //    _culinaryPortalRepository.DeleteRecipeIngredient(exRecipeIngredient);
                //}

                ////add all new
                //foreach (var newRecipeIngredient in recipe.RecipeIngredients)
                //{                    
                //    recipe.RecipeIngredients.Add(newRecipeIngredient);
                //}
                // _culinaryPortalRepository.UpdateRecipe(recipe);
                //foreach (var newRecipeIngredient in recipe.RecipeIngredients)
                //{
                //    _culinaryPortalRepository.AddRecipeIngredient(newRecipeIngredient);
                //}  

                await _culinaryPortalRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }
            return NoContent();
        }
    }
}
