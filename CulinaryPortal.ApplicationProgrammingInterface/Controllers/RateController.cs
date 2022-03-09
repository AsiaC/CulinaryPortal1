using CulinaryPortal.Application.Features.Rates.Commands.CreateRate;
using CulinaryPortal.Application.Features.Rates.Commands.DeleteRate;
using CulinaryPortal.Application.Features.Rates.Queries.GetRateDetail;
using CulinaryPortal.Application.Features.Rates.Queries.GetRatesList;
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
               
        [HttpGet]
        public async Task<ActionResult<List<RateDto>>> GetRates()
        {
            try
            {
                var dtos = await _mediator.Send(new GetRatesListQuery());
                return Ok(dtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{rateId}", Name = "GetRate")]
        public async Task<ActionResult<RateDto>> GetRate([FromRoute] int rateId)
        {
            try
            {
                var getRateDetailQuery = new GetRateDetailQuery() { Id = rateId };
                var rate = await _mediator.Send(getRateDetailQuery);
                if (rate == null)
                {
                    return NotFound();
                }
                return Ok(rate);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RateDto>> CreateRate([FromBody] CreateRateCommand createRateCommand)
        {
            try
            {
                var objectToReturn = await _mediator.Send(createRateCommand);
                return CreatedAtAction(nameof(GetRate), objectToReturn);
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
