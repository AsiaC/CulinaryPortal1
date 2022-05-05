using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.Domain.Entities
{
    [Table("Photos")]
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] ContentPhoto { get; set; }

        [Required]
        public bool IsMain { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
