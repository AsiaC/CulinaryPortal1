using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Domain.Entities
{
    public class ShoppingList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public IList<ListItem> Items { get; set; }
    }
}
