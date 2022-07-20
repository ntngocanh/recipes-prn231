using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class Collection
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CollectionRecipe> CollectionRecipes { get; set; }
    }
}
