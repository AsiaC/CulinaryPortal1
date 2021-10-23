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
{// TODO CHYBA TEGO NIE POTRZEBUJĘ ZBADAJ FUNKCJE WEWNATRZ MOZE TEŻ SĄ DO USUNIECIA
    [Route("api/instructions")]
    [Authorize]
    [ApiController]
    public class InstructionsController : ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;

        public InstructionsController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/instructions
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<InstructionDto>>> GetInstructions()
        //{
        //    try
        //    {
        //        var instructionFromRepo = await _culinaryPortalRepository.GetInstructionsAsync();
        //        return Ok(_mapper.Map<IEnumerable<InstructionDto>>(instructionFromRepo));
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e);
        //    }            
        //}

        //// GET: api/instructions/5
        //[HttpGet("{instructionId}", Name = "GetInstruction")]
        //public async Task<ActionResult<InstructionDto>> GetInstruction([FromRoute] int instructionId)
        //{
        //    try
        //    {
        //        var instructionFromRepo = await _culinaryPortalRepository.GetInstructionAsync(instructionId);
        //        if (instructionFromRepo == null)
        //        {
        //            return NotFound();
        //        }                
        //        return Ok(_mapper.Map<InstructionDto>(instructionFromRepo));
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e);
        //    }            
        //}

        ////POST: api/instructions
        //[HttpPost]
        //public async Task<ActionResult<Instruction>> CreateInstruction([FromBody] InstructionDto instructionDto)
        //{
        //    try
        //    {
        //        var instruction = _mapper.Map<Instruction>(instructionDto);
        //        await _culinaryPortalRepository.AddInstructionAsync(instruction);
        //        return CreatedAtAction(nameof(GetInstruction), new { instructionId = instruction.Id }, instruction);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e);
        //    }            
        //}

        // DELETE: api/instructions/5 
        //[HttpDelete("{instructionId}")]
        //public async Task<ActionResult> DeleteInstruction([FromRoute] int instructionId)
        //{
        //    try
        //    {
        //        var instructionFromRepo = await _culinaryPortalRepository.GetInstructionAsync(instructionId);
        //        if (instructionFromRepo == null)
        //        {
        //            return NotFound();
        //        }
        //        await _culinaryPortalRepository.DeleteInstructionAsync(instructionFromRepo);

        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e);
        //    }           
        //}
    }
}
