using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Models
{
    public class PhotoDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public byte[] ContentPhoto { get; set; }

        public bool IsMain { get; set; }
    }
}
