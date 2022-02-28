using CulinaryPortal.Application.Features.Measures.Queries.GetMeasuresList;
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
    [Route("api/measures")]
    [ApiController]
    [Authorize]
    public class MeasureController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MeasureController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<MeasureDto>>> GetMeasures()
        {
            try
            {
                GetMeasuresListQuery getMeasuresListQuery = new GetMeasuresListQuery();
                var measures = await _mediator.Send(getMeasuresListQuery);
                return Ok(measures);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
