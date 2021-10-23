using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace CulinaryPortal.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/photos")]
    [Authorize]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;

        public PhotosController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //TODO IActionResult zmin
        //[HttpPost]
        //public async Task<IActionResult> UploadImage() 
        //{//http://www.binaryintellect.net/articles/2f55345c-1fcb-4262-89f4-c4319f95c5bd.aspx
        //    try
        //    {
        //        foreach (var file in Request.Form.Files)
        //        {
        //            Photo photo = new Photo();
        //            photo.Description = file.FileName;

        //            MemoryStream ms = new MemoryStream();
        //            file.CopyTo(ms);
        //            photo.ContentPhoto = ms.ToArray();

        //            ms.Close();
        //            ms.Dispose();

        //            await _culinaryPortalRepository.AddPhotoAsync(photo);                    
        //        }
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e);                
        //    }            
        //}

        //[HttpPost]
        //public async Task<IActionResult> UploadImage(IFormFile upload)
        //{//https://www.mikesdotnetting.com/article/259/asp-net-mvc-5-with-ef-6-working-with-files
        //    try
        //    {
        //        if (upload != null && upload.Length > 0)
        //        {
        //            Photo photo = new Photo
        //            {
        //                Description = upload.FileName,
        //                //ContentPhoto = file.ContentType
        //            };
        //            using (var reader = new System.IO.BinaryReader(upload.OpenReadStream()))
        //            {
        //                photo.ContentPhoto = reader.ReadBytes((int)upload.Length);
        //            }                   

        //            await _culinaryPortalRepository.AddPhotoAsync(photo);
        //        }
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e);
        //    }
        //}



        //[HttpPost]
        //public async Task<IActionResult> UploadImage(IFormFile upload)
        //{//https://docs.microsoft.com/pl-pl/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0
        //    try
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await upload.CopyToAsync(memoryStream);

        //            // Upload the file if less than 2 MB
        //            if (memoryStream.Length < 2097152)
        //            {
        //                var photo = new Photo()
        //                {
        //                    ContentPhoto = memoryStream.ToArray(),
        //                    IsMain = true,    
        //                    RecipeId = 8
        //                };

        //                await _culinaryPortalRepository.AddPhotoAsync(photo);
        //                return Ok();
        //            }
        //            else
        //            {
        //                //ModelState.AddModelError("File", "The file is too large.");
        //                return BadRequest();
        //            }
        //        }               
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e);
        //    }
        //}

        // DELETE: api/photos/1
        [HttpDelete("{photoId}")]
        public async Task<ActionResult> DeletePhoto([FromRoute] int photoId)
        {
            try
            {
                var photoFromRepo = await _culinaryPortalRepository.GetPhotoAsync(photoId);
                if (photoFromRepo == null)
                {
                    return NotFound();
                }
                await _culinaryPortalRepository.DeletePhotoAsync(photoFromRepo);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // PUT: api/photos/5
        [HttpPut("{photoId}")]
        public async Task<ActionResult> UpdatePhoto([FromRoute] int photoId, [FromBody] PhotoDto photoDto)
        {
            if (photoId != photoDto.Id)
            {
                return BadRequest();
            }
            try
            {
                var existingPhotoData = await _culinaryPortalRepository.GetPhotoAsync(photoId);
                if (existingPhotoData == null)
                {
                    return NotFound();
                }                

                if (existingPhotoData.IsMain != photoDto.IsMain)
                    existingPhotoData.IsMain = photoDto.IsMain;

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
