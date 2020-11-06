using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class ShoppingList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //id INT NOT NULL PRIMARY KEY IDENTITY
        public int Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }        
        
        public int UserId { get; set; }
        
        public List<string> Positions { get; set; }
    }
}
