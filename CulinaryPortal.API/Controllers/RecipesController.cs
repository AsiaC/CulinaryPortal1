using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                var recipesFromRepo = await _culinaryPortalRepository.GetRecipesAsync();
                return Ok(_mapper.Map<IEnumerable<RecipeDto>>(recipesFromRepo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        // GET: api/recipes/5
        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public async Task<ActionResult<RecipeDto>> GetRecipe([FromRoute] int recipeId)
        {
            try
            {
                var recipeFromRepo = await _culinaryPortalRepository.GetRecipeAsync(recipeId);
                if (recipeFromRepo == null)
                {
                    return NotFound();
                }                
                var recipe = _mapper.Map<RecipeDto>(recipeFromRepo);
                recipe.CountCookbooks = await _culinaryPortalRepository.CountAssociatedCookbooksAsync(recipeId);
                return Ok(recipe);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        //POST: api/recipes
        [HttpPost]
        public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] RecipeDto recipeDto)
        {
            try
            {
                var recipe = _mapper.Map<Recipe>(recipeDto);
                var i = 0;
                foreach (var instruction in recipe.Instructions)
                {
                    i += 1;
                    if (instruction.Step == 0)
                    {
                        instruction.Step = i;
                    }
                }

                await _culinaryPortalRepository.AddRecipeAsync(recipe);

                return CreatedAtAction(nameof(GetRecipe), new { recipeId = recipe.Id }, recipe);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }      
        }

        // DELETE: api/recipes/5
        [HttpDelete("{recipeId}")]
        public async Task<ActionResult> DeleteRecipe([FromRoute] int recipeId)
        {
            try
            {
                var recipeFromRepo = await _culinaryPortalRepository.GetRecipeAsync(recipeId);
                if (recipeFromRepo == null)
                {
                    return NotFound(); 
                }
                await _culinaryPortalRepository.DeleteRecipeAsync(recipeFromRepo);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // PUT: api/recipes/5
        [HttpPut("{recipeId}")]
        public async Task<ActionResult> UpdateRecipe([FromRoute] int recipeId, [FromBody] RecipeDto recipeDto)
        {            
            if (recipeId != recipeDto.Id)
            {
                return BadRequest();
            }
            try
            {
                var existingRecipe = await _culinaryPortalRepository.GetRecipeAsync(recipeId);
                if (existingRecipe == null)
                {
                    return NotFound();
                }                

                if (existingRecipe.Name != recipeDto.Name)
                    existingRecipe.Name = recipeDto.Name;
                if (existingRecipe.Description != recipeDto.Description)
                    existingRecipe.Description = recipeDto.Description;
                if (existingRecipe.DifficultyLevel != recipeDto.DifficultyLevel)
                    existingRecipe.DifficultyLevel = recipeDto.DifficultyLevel;
                if (existingRecipe.PreparationTime != recipeDto.PreparationTime)
                    existingRecipe.PreparationTime = recipeDto.PreparationTime;
                if (existingRecipe.CategoryId != recipeDto.CategoryId)
                    existingRecipe.CategoryId = recipeDto.CategoryId;
                //Rate, photos

                //Instructions
                List<Instruction> copyExInstructions = new List<Instruction>();
                copyExInstructions.AddRange(existingRecipe.Instructions);
                foreach (var exInstruction in copyExInstructions)
                {
                    var checkIfInstructionExist = recipeDto.Instructions.Any(i => i.Id == exInstruction.Id);
                    if (checkIfInstructionExist)
                    {
                        var instructionDto = recipeDto.Instructions.FirstOrDefault(i => i.Id == exInstruction.Id);
                        if (exInstruction.Name != instructionDto.Name)
                            exInstruction.Name = instructionDto.Name;
                        if (exInstruction.Description != instructionDto.Description)
                            exInstruction.Description = instructionDto.Description;
                    }
                    else
                    {
                        existingRecipe.Instructions.Remove(exInstruction);
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
                    //nie mozna uzyć mapera? //var newMeasure = _mapper.Map<Measure>(newRecipeIngredient.Measure);
                    //var exMeasure = await _culinaryPortalRepository.GetMeasureAsync(newRecipeIngredient.Measure.Id); OST ZAKOMENTOWANE
                    //var exIngredient = await _culinaryPortalRepository.GetIngredientAsync(newRecipeIngredient.Ingredient.Id); OST ZAKOMENTOWANE

                    var exMeasure = await _culinaryPortalRepository.GetMeasureAsync(newRecipeIngredient.MeasureId);
                    var exIngredient = await _culinaryPortalRepository.GetIngredientAsync(newRecipeIngredient.IngredientId);

                    var newToAdd = new RecipeIngredient()
                    {
                        Quantity = newRecipeIngredient.Quantity,
                        //MeasureId = newRecipeIngredient.Measure.Id,      OST ZAKOMENTOWANE
                        //IngredientId = newRecipeIngredient.Ingredient.Id, OST ZAKOMENTOWANE
                        MeasureId = newRecipeIngredient.MeasureId,
                        IngredientId = newRecipeIngredient.IngredientId,
                        //Measure = _mapper.Map<Measure>(newRecipeIngredient.Measure),
                        //Ingredient = _mapper.Map<Ingredient>(newRecipeIngredient.Ingredient),
                        //Measure = exMeasure,                        OST ZAKOMENTOWANE
                        //Ingredient = exIngredient, OST ZAKOMENTOWANE
                        RecipeId = recipeId
                    };
                    existingRecipe.RecipeIngredients.Add(newToAdd);
                }

                await _culinaryPortalRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        //api/recipes/search
        //[HttpGet("search")]
        [HttpPut("search")]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> SearchRecipes([FromBody] SearchRecipeDto searchRecipeDto)
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

        // GET: api/recipes/3/photos
        [HttpGet("{recipeId}/photos", Name = "GetRecipePhotos")]
        public async Task<IActionResult> GetRecipePhotosAsync([FromRoute] int recipeId)
        {
            try
            {
                var photosFromRepo = await _culinaryPortalRepository.GetRecipePhotosAsync(recipeId);
                if (photosFromRepo.Any())
                {
                    return Ok(_mapper.Map<IEnumerable<PhotoDto>>(photosFromRepo));
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
        

        // POST: api/recipes/3/photos
        [HttpPost("{recipeId}/photos")]
        public async Task<IActionResult> UploadImage([FromRoute] int recipeId, IFormFile upload)
        {//https://docs.microsoft.com/pl-pl/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0
            try
            {
                if (upload != null && upload.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await upload.CopyToAsync(memoryStream);

                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 2097152)
                        {
                            var photo = new Photo()
                            {
                                ContentPhoto = memoryStream.ToArray(),
                                IsMain = true,
                                RecipeId = recipeId,
                            };

                            await _culinaryPortalRepository.AddPhotoAsync(photo);
                            return Ok();
                        }
                        else
                        {
                            //ModelState.AddModelError("File", "The file is too large.");
                            return BadRequest();
                        }
                    }
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        //// DELETE: api/recipes/3/photos/1
        //[HttpDelete("{photoId}")]
        //public async Task<ActionResult> DeletePhoto([FromRoute] int photoId)
        //{
        //    try
        //    {
        //        var photoFromRepo = await _culinaryPortalRepository.GetPhotoAsync(photoId);
        //        if (photoFromRepo == null)
        //        {
        //            return NotFound();
        //        }
        //        await _culinaryPortalRepository.DeletePhotoAsync(photoFromRepo);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e);
        //    }
        //}


        // PUT: api/recipes/3/photos
        [HttpPut("{recipeId}/photos")]
        public async Task<IActionResult> UpdateMainPhoto([FromRoute] int recipeId, [FromBody] int photoId)
        {
            try
            {
                var photosFromRepo = await _culinaryPortalRepository.GetRecipePhotosAsync(recipeId);
                if (!photosFromRepo.Any())
                {
                    return NotFound();
                }
                var newMainPhoto = photosFromRepo.FirstOrDefault(p => p.Id == photoId);
                if (newMainPhoto == null)
                {
                    return NotFound();
                }
                newMainPhoto.IsMain = true;

                var otherPhotos = photosFromRepo.Where(p => p.Id != photoId);
                if (otherPhotos.Any())
                {
                    foreach (var otherPhoto in otherPhotos)
                    {
                        otherPhoto.IsMain = false;
                    }
                }               
                
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
