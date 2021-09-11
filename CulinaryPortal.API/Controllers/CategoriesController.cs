using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Controllers
{

    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        public CategoriesController(ICulinaryPortalRepository culinaryPortalRepository )
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
           
        }
        // GET: api/categories
        //[AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            try
            {
                var categoriesFromRepo = await _culinaryPortalRepository.GetCategoriesAsync();
                return Ok(categoriesFromRepo);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }
    }
}
