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
    [Route("api/instructions")]
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instruction>>> GetInstructions()
        {
            var instructionFromRepo = await _culinaryPortalRepository.GetInstructionsAsync();
            return Ok(_mapper.Map<Models.InstructionDto>(instructionFromRepo));
        }

        // GET: api/instructions/5
        [HttpGet("{instructionId}", Name = "GetInstruction")]
        public async Task<ActionResult<Instruction>> GetInstruction(int instructionId)
        {
            var checkIfInstructionExists =await _culinaryPortalRepository.InstructionExistsAsync(instructionId);

            if (checkIfInstructionExists == false)
            {
                return NotFound();
            }
            var instructionFromRepo = await _culinaryPortalRepository.GetInstructionAsync(instructionId);
            return Ok(_mapper.Map<Models.InstructionDto>(instructionFromRepo));
        }

        [HttpPost]
        public ActionResult<Instruction> CreateInstruction(Instruction instruction)
        {
            _culinaryPortalRepository.AddInstruction(instruction);
            _culinaryPortalRepository.Save();

            return CreatedAtAction("GetIngredient", new { instructionId = instruction.Id }, instruction);
        }

        // DELETE: api/instructions/5
        [HttpDelete("{instructionId}")]
        public async Task<ActionResult> DeleteInstruction(int instructionId)
        {
            var instructionFromRepo = await _culinaryPortalRepository.GetInstructionAsync(instructionId);
            if (instructionFromRepo == null)
            {
                return NotFound();
            }

            _culinaryPortalRepository.DeleteInstruction(instructionFromRepo);
            _culinaryPortalRepository.Save();

            return NoContent();
        }
    }
}
