using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //id INT NOT NULL PRIMARY KEY IDENTITY
        public int Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        
        public int UserId { get; set; }
        
        //[ForeignKey("UserId")]
        //public User User { get; set; }        
        
        public double Rate { get; set; } //DEFAULT 0
        
        [MaxLength(100)]
        public string Description { get; set; }
        //lista instukcji
        
        public ICollection<Instruction> Instructions { get; set; }
        
        public ICollection<Photo> Photos { get; set; }
    }
}
