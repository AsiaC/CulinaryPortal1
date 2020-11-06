using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class Instruction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int Step { get; set; }
        
        public int RecipeId { get; set; }
        
        [MaxLength(30)]
        public string Name { get; set; }
        
        [MaxLength(150)]
        public string Description { get; set; }
    }
}
