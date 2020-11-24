using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CulinaryPortal.API.Controllers
{
    [Route("api/measures")]
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
        public async Task<ActionResult<IEnumerable<Measure>>> GetMeasures()
        {
            var measuresFromRepo = await _culinaryPortalRepository.GetMeasuresAsync();
            return Ok(_mapper.Map<Models.MeasureDto>(measuresFromRepo));
        }

        // GET: api/measures/5
        [HttpGet("{measureId}", Name = "GetMeasure")]
        public async Task<ActionResult<Measure>> GetMeasureAsync(int measureId)
        {
            var checkIfMeasureExists = await _culinaryPortalRepository.MeasureExistsAsync(measureId);

            if (checkIfMeasureExists == false)
            {
                return NotFound();
            }
            var measureFromRepo =await _culinaryPortalRepository.GetMeasureAsync(measureId);
            return Ok(_mapper.Map<Models.MeasureDto>(measureFromRepo));
        }

        [HttpPost]
        public ActionResult<Measure> CreateMeasure(Measure measure)
        {
            _culinaryPortalRepository.AddMeasure(measure);
            _culinaryPortalRepository.Save();

            return CreatedAtAction("GetMeasure", new { measureId = measure.Id }, measure);
        }

        // DELETE: api/measures/5
        [HttpDelete("{measureId}")]
        public async Task<ActionResult> DeleteMeasure(int measureId)
        {
            var measureFromRepo =await _culinaryPortalRepository.GetMeasureAsync(measureId);
            if (measureFromRepo == null)
            {
                return NotFound();
            }

            _culinaryPortalRepository.DeleteMeasure(measureFromRepo);
            _culinaryPortalRepository.Save();

            return NoContent();
        }

    }
}
