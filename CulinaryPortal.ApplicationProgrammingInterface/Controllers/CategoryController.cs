using CulinaryPortal.Application.Features.Categories.Queries.GetCategoriesList;
using CulinaryPortal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.ApplicationProgrammingInterface.Controllers
{
    [Route("api/categories")]
    [ApiController]
    //[Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            try
            { //todo nie wiem czy tu try catch czy gdzies indziej
                GetCategoriesListQuery getCategoriesListQuery = new GetCategoriesListQuery();
                var categories = await _mediator.Send(getCategoriesListQuery);
                return Ok(categories);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
