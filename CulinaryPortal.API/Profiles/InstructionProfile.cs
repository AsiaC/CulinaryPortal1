using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Profiles
{
    public class InstructionProfile: Profile
    {
        public InstructionProfile()
        {
            CreateMap<Entities.Instruction, Models.InstructionDto>();
        }
    }
}
