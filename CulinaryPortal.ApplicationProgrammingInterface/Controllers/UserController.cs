using CulinaryPortal.Application.Features.Users.Commands.DeleteUser;
using CulinaryPortal.Application.Features.Users.Queries.GetUserDetail;
using CulinaryPortal.Application.Features.Users.Queries.GetUsersList;
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
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "OnlyAdminRole")]
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            try
            {
                var dtos = await _mediator.Send(new GetUsersListQuery());
                return Ok(dtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        { //todo czy try catch tu potrzeba?
            var getUserDetailQuery = new GetUserDetailQuery() { Id = userId };
            var recipe = await _mediator.Send(getUserDetailQuery);
            if (recipe == null) //todo nie jestem pewna czy null, czy coś innego bo w base repo jest Find a nie FirstOrDetail
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpDelete("{userId}", Name = "DeleteUser")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var deleteUserCommand = new DeleteUserCommand() { Id = userId };
            await _mediator.Send(deleteUserCommand);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateShoppingList([FromBody] UserUpdateDto userUpdateDto) //todo nie jestem pewna typu czy nie powinien być command
        {
            await _mediator.Send(userUpdateDto);
            return NoContent();
        }
    }   
}
