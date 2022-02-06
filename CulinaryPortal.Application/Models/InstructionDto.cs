using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Models
{
    public class InstructionDto
    {
        public int Id { get; set; }

        public int Step { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
