using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class Instruction
    {
        [Key]
        public int Id { get; set; }
        
        public int Step { get; set; }     
       
        [MaxLength(30)]
        public string Name { get; set; }
        
        [MaxLength(1500)]
        public string Description { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
