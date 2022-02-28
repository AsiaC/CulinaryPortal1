using CulinaryPortal.Application.Features.Cookbooks.Commands.DeleteCookbook;
using CulinaryPortal.Application.Features.Cookbooks.Queries.GetCookbookDetail;
using CulinaryPortal.Application.Features.Cookbooks.Queries.GetCookbooksList;
using CulinaryPortal.Application.Models;
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
    [Route("api/cookbooks")]
    [ApiController]
    [Authorize]
    public class CookbookController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CookbookController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Policy = "OnlyAdminRole")]
        [HttpGet]
        public async Task<ActionResult<List<CookbookDto>>> GetCookbooks()
        {
            try
            {
                GetCookbooksListQuery getCookbooksListQuery = new GetCookbooksListQuery();
                var cookbooks = await _mediator.Send(getCookbooksListQuery);
                return Ok(cookbooks);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{cookbookId}", Name = "GetCookbook")]
        public async Task<ActionResult<CookbookDto>> GetCookbook([FromRoute] int cookbookId)
        {
            try
            {
                GetCookbookDetailQuery getCookbookDetailQuery = new GetCookbookDetailQuery() { Id = cookbookId };
                var cookbook = await _mediator.Send(getCookbookDetailQuery);
                if (cookbook == null)
                {
                    return NotFound();
                }
                return Ok(cookbook);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CookbookDto>> CreateCookbook([FromBody] CookbookDto cookbookDto) //todo nie jestem pewna typu czy nie powinien być command
        {
            var objectToReturn = await _mediator.Send(cookbookDto);

            return CreatedAtAction(nameof(GetCookbook), objectToReturn);
        }


        [HttpDelete("{cookbookId}")]
        public async Task<ActionResult> DeleteCookbook([FromRoute] int cookbookId)
        {
            var deleteCommand = new DeleteCookbookCommand() { Id = cookbookId };
            await _mediator.Send(deleteCommand);
            return NoContent();
        }
    }
}
