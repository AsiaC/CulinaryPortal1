using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
                       
        public byte[] ContentPhoto { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
