using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class Cookbook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //id INT NOT NULL PRIMARY KEY IDENTITY
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
        
        public int UserId { get; set; }
        
        [MaxLength(180)]
        public string Description { get; set; }
    }
}
