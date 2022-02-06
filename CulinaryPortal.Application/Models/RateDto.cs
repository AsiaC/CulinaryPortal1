using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Models
{
    public class RateDto
    {
        public int? Id { get; set; }
        public int Value { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
    }
}
