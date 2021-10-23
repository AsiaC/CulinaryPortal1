using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CulinaryPortal.API.Controllers
{
    [Route("api/measures")]
    [Authorize]
    [ApiController]
    public class MeasuresController : ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;

        public MeasuresController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/measures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasureDto>>> GetMeasures()
        {
            try
            {
                var measuresFromRepo = await _culinaryPortalRepository.GetMeasuresAsync();
                return Ok(_mapper.Map<IEnumerable<Models.MeasureDto>>(measuresFromRepo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        // GET: api/measures/5
        [HttpGet("{measureId}", Name = "GetMeasure")]
        public async Task<ActionResult<MeasureDto>> GetMeasure([FromRoute] int measureId)
        {
            try
            {
                var measureFromRepo = await _culinaryPortalRepository.GetMeasureAsync(measureId);
                if (measureFromRepo == null)
                {
                    return NotFound();
                }                
                return Ok(_mapper.Map<Models.MeasureDto>(measureFromRepo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        [HttpPost]
        public async Task<ActionResult<Measure>> CreateMeasure([FromBody] MeasureDto measureDto)
        {
            try
            {
                var measure = _mapper.Map<Measure>(measureDto);
                await _culinaryPortalRepository.AddMeasureAsync(measure);

                return CreatedAtAction(nameof(GetMeasure), new { measureId = measure.Id }, measure);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        // DELETE: api/measures/5
        //[HttpDelete("{measureId}")]
        //public async Task<ActionResult> DeleteMeasure([FromRoute] int measureId)
        //{
        //    try
        //    {
        //        var measureFromRepo =await _culinaryPortalRepository.GetMeasureAsync(measureId);
        //        if (measureFromRepo == null)
        //        {
        //            return NotFound();
        //        }
        //        await _culinaryPortalRepository.DeleteMeasureAsync(measureFromRepo);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e);
        //    }
        //}

    }
}
