using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Http;

namespace CulinaryPortal.API.Controllers
{
    [Route("api/rates")]
    [ApiController]
    public class RateController : Controller
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;

        public RateController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/rates
        //[AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RateDto>>> GetRates()
        {
            try
            {
                var ratesFromRepo = await _culinaryPortalRepository.GetRatesAsync();
                return Ok(_mapper.Map<IEnumerable<RateDto>>(ratesFromRepo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // GET: api/rates/5
        [HttpGet("{rateId}", Name = "GetRate")]
        public async Task<ActionResult<RateDto>> GetRate([FromRoute] int rateId)
        {
            try
            {
                var rateFromRepo = await _culinaryPortalRepository.GetRateAsync(rateId);
                if (rateFromRepo == null)
                {
                    return NotFound();
                }
                var rate = _mapper.Map<RateDto>(rateFromRepo);

                return Ok(rate);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        //POST: api/rates
        [HttpPost()]
        public async Task<IActionResult> CreateRate([FromBody] RateDto rating)
        {
            try
            {
                Rate newRate = _mapper.Map<Rate>(rating);
                //existingRecipe.Rates.Add(newRate); //TODO nie trzeba robi update Recipe? nie trzeba dodawa tego w repository? mozna w kontrolerze?                        
                await _culinaryPortalRepository.AddRateAsync(newRate);
                return CreatedAtAction(nameof(GetRate), new { rateId = newRate.Id }, newRate);
                //return Ok();//TODO czy zwracac Ok czy Create?
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
                var rateFromRepo = await _culinaryPortalRepository.GetRateAsync(rateId);
                if (rateFromRepo == null)
                {
                    return NotFound();
                }
                await _culinaryPortalRepository.DeleteRateAsync(rateFromRepo);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
