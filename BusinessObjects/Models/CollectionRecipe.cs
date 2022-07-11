using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class CollectionRecipe
    {
        [Key, Column(Order = 0)]
        public int CollectionId { get; set; }
        [Key, Column(Order = 1)]
        public int RecipeId { get; set; }
        [ForeignKey("CollectionId")]
        public virtual Collection Collection { get; set; }
        [ForeignKey("RecipeId")]
        public virtual Recipe Recipe { get; set; }
    }
}
