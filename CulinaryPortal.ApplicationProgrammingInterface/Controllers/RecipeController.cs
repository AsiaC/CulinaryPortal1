using CulinaryPortal.Application.Features.Photos.Commands.CreatePhoto;
using CulinaryPortal.Application.Features.Photos.Commands.DeletePhoto;
using CulinaryPortal.Application.Features.Photos.Commands.UpdatePhoto;
using CulinaryPortal.Application.Features.Photos.Queries.GetPhotoDetail;
using CulinaryPortal.Application.Features.Recipes.Commands.CreateRecipe;
using CulinaryPortal.Application.Features.Recipes.Commands.DeleteRecipe;
using CulinaryPortal.Application.Features.Recipes.Commands.UpdateRecipe;
using CulinaryPortal.Application.Features.Recipes.Queries.GetRecipeDetail;
using CulinaryPortal.Application.Features.Recipes.Queries.GetRecipePhotos;
using CulinaryPortal.Application.Features.Recipes.Queries.GetRecipesList;
using CulinaryPortal.Application.Features.Recipes.Queries.SearchRecipes;
using CulinaryPortal.Application.Models;
using CulinaryPortal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.ApplicationProgrammingInterface.Controllers
{
    [Route("api/recipes")]
    [AllowAnonymous]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet(Name = "GetRecipes")]
        public async Task<ActionResult<List<RecipeDto>>> GetRecipes()
        {
            try
            {
                var dtos = await _mediator.Send(new GetRecipesListQuery());
                return Ok(dtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public async Task<ActionResult<RecipeDto>> GetRecipe([FromRoute] int recipeId)
        {
            try
            {
                var getRecipeDetailQuery = new GetRecipeDetailQuery() { Id = recipeId };
                var recipe = await _mediator.Send(getRecipeDetailQuery);
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

        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] CreateRecipeCommand createRecipeCommand)
        {
            try
            {
                var objectToReturn = await _mediator.Send(createRecipeCommand);
                if (objectToReturn.Id == null)
                {
                    throw new Exception("Server error while creating a recipe");
                }      
                return Ok(objectToReturn);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        [Authorize]
        [HttpDelete("{recipeId}", Name = "DeleteRecipe")]
        public async Task<ActionResult> DeleteRecipe([FromRoute] int recipeId)
        {
            try
            {
                var deleteRecipeCommand = new DeleteRecipeCommand() { Id = recipeId };
                await _mediator.Send(deleteRecipeCommand);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        [Authorize(Roles = "Member")]
        [HttpPut("{recipeId}")]
        public async Task<ActionResult> UpdateRecipe([FromRoute] int recipeId, [FromBody] UpdateRecipeCommand updateRecipeCommand)
        {
            try
            {
                await _mediator.Send(updateRecipeCommand);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        [HttpGet("search", Name = "Search")]
        public async Task<ActionResult<List<RecipeDto>>> SearchRecipes(string name, int? categoryId, int? difficultyLevelId, int? preparationTimeId, int? userId, int? top)
        {
            try
            {
                var searchRecipesQuery = new GetRecipesListQuery() { Name = name, CategoryId = categoryId, DifficultyLevelId = difficultyLevelId, PreparationTimeId = preparationTimeId, UserId = userId, Top = top };
                var dtos = await _mediator.Send(searchRecipesQuery);
                return Ok(dtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // GET: api/recipes/3/photos
        [Authorize(Roles = "Member")]
        [HttpGet("{recipeId}/photos", Name = "GetRecipePhotos")]
        public async Task<ActionResult<List<PhotoDto>>> GetRecipePhotosAsync([FromRoute] int recipeId)
        {
            try
            {
                var getRecipePhotosQuery = new GetRecipePhotosQuery() { RecipeId = recipeId };
                var photos = await _mediator.Send(getRecipePhotosQuery);
                if (photos.Any())
                {
                    return Ok(photos);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }


        // POST: api/recipes/3/photos
        [Authorize(Roles = "Member")]
        [HttpPost("{recipeId}/photos")]
        public async Task<ActionResult> UploadImage([FromRoute] int recipeId, IFormFile upload)
        {//https://docs.microsoft.com/pl-pl/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0
            try
            {
                if (upload != null && upload.Length > 0)
                {
                    //Check if photo should be the main or not
                    bool isMainPhoto = true;

                    var getRecipePhotosQuery = new GetRecipePhotosQuery() { RecipeId = recipeId };
                    var allRecipePhotos = await _mediator.Send(getRecipePhotosQuery);                    
                    if (allRecipePhotos.Any())
                        isMainPhoto = false;

                    using (var memoryStream = new MemoryStream())
                    {
                        await upload.CopyToAsync(memoryStream);

                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 2097152)
                        {
                            var createPhotoCommand = new CreatePhotoCommand()
                            {
                                ContentPhoto = memoryStream.ToArray(),
                                IsMain = isMainPhoto,
                                RecipeId = recipeId,
                            };

                            var addedPhoto = await _mediator.Send(createPhotoCommand);
                            return Ok();
                        }
                        else
                        {
                            throw new Exception("Server error while adding a photo. The file is too large.");
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

        [Authorize(Roles = "Member")]
        [HttpPut("{recipeId}/photos")]
        public async Task<ActionResult> UpdateMainPhoto([FromRoute] int recipeId, [FromBody] int photoId)
        {
            try
            {
                var getRecipePhotosQuery = new GetRecipePhotosQuery() { RecipeId = recipeId };
                var allRecipePhotos = await _mediator.Send(getRecipePhotosQuery);
                if (!allRecipePhotos.Any())
                {
                    return NotFound();
                }
                var newMainPhoto = allRecipePhotos.FirstOrDefault(p => p.Id == photoId);
                if (newMainPhoto == null)
                {
                    return NotFound();
                }
                var updateMainPhotoCommand = new UpdatePhotoCommand()
                {
                    Id = newMainPhoto.Id,
                    ContentPhoto = newMainPhoto.ContentPhoto,
                    IsMain = true
                };                
                await _mediator.Send(updateMainPhotoCommand);

                var otherPhotos = allRecipePhotos.Where(p => p.Id != photoId && p.IsMain == true);
                if (otherPhotos.Any())
                {
                    foreach (var otherPhoto in otherPhotos)
                    {
                        var updatePhotoCommand = new UpdatePhotoCommand()
                        {
                            Id = otherPhoto.Id,
                            ContentPhoto = otherPhoto.ContentPhoto,
                            IsMain = false
                        };
                        await _mediator.Send(updatePhotoCommand);                        
                    }
                }                
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [Authorize(Roles = "Member")]
        // DELETE: api/recipes/3/photos/1
        [HttpDelete("{recipeId}/photos/{photoId}")]
        public async Task<ActionResult> DeletePhoto([FromRoute] int photoId)
        {
            try
            {
                var deleteCommand = new DeletePhotoCommand() { Id = photoId };
                await _mediator.Send(deleteCommand);                
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }       
    }
}
