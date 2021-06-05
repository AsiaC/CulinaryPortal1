using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class ListItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }       

        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
    }
}
