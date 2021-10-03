using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    [Table("Photos")] //nazwa tabeli Photos, nazwa klasy Photo
    public class Photo
    {
        [Key]
        public int Id { get; set; }
                       
        public byte[] ContentPhoto { get; set; }

        public bool IsMain { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
