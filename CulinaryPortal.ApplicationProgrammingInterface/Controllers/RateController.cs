using CulinaryPortal.Application.Features.Rates.Commands.CreateRate;
using CulinaryPortal.Application.Features.Rates.Commands.DeleteRate;
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
    [Route("api/rates")]
    [ApiController]
    [Authorize]
    public class RateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RateController(IMediator mediator)
        {
            _mediator = mediator;
        }           

        [HttpPost]
        public async Task<ActionResult<RateDto>> CreateRate([FromBody] CreateRateCommand createRateCommand)
        {
            try
            {
                var objectToReturn = await _mediator.Send(createRateCommand);       
                if (objectToReturn.Id == null)
                {
                    throw new Exception("Server error while creating a rate");
                }
                return Ok(objectToReturn);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }
       

        [HttpDelete("{rateId}")]
        public async Task<ActionResult> DeleteRate([FromRoute] int rateId)
        {
            try
            {
                var deleteCommand = new DeleteRateCommand() { Id = rateId };
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
