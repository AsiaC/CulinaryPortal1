using CulinaryPortal.Application.Features.ShoppingLists.Commands.CreateShoppingList;
using CulinaryPortal.Application.Features.ShoppingLists.Commands.DeleteShoppingList;
using CulinaryPortal.Application.Features.ShoppingLists.Commands.UpdateShoppingList;
using CulinaryPortal.Application.Features.ShoppingLists.Queries.GetShoppingListDetail;
using CulinaryPortal.Application.Features.ShoppingLists.Queries.GetShoppingListsList;
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
    [Route("api/shoppingLists")]
    [ApiController]
    [AllowAnonymous]
    public class ShoppingListController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShoppingListController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/shoppingLists
        [Authorize(Policy = "OnlyAdminRole")]
        [HttpGet]
        public async Task<ActionResult<List<ShoppingListDto>>> GetShoppingLists()
        {
            try
            {
                var dtos = await _mediator.Send(new GetShoppingListsListQuery());
                return Ok(dtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // GET: api/shoppingLists/5
        [HttpGet("{shoppingListId}", Name = "GetShoppingList")]
        public async Task<ActionResult<ShoppingListDto>> GetShoppingList([FromRoute] int shoppingListId)
        {
            try
            {
                var getShoppligListDetailQuery = new GetShoppingListDetailQuery() { Id = shoppingListId };
                var shoppingList = await _mediator.Send(getShoppligListDetailQuery);
                if (shoppingList == null)
                {
                    return NotFound();
                }
                return Ok(shoppingList);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingListDto>> CreateShoppingList([FromBody] CreateShoppingListCommand createShoppingListCommand)
        {
            var shoppingListToReturn = await _mediator.Send(createShoppingListCommand);
            
            return CreatedAtAction(nameof(GetShoppingList), shoppingListToReturn);
        }

        [HttpPut("{shoppingListId}")]        
        public async Task<ActionResult> UpdateShoppingList([FromRoute] int shoppingListId, [FromBody] UpdateShoppingListCommand updateShoppingListCommand)
        {
            await _mediator.Send(updateShoppingListCommand);
            return NoContent();
        }


        [HttpDelete("{shoppingListId}")]
        public async Task<ActionResult> DeleteShoppingList([FromRoute] int shoppingListId)
        {
            var deleteCommand = new DeleteShoppingListCommand() { Id = shoppingListId };
            await _mediator.Send(deleteCommand);
            return NoContent();
        }
    }
}
