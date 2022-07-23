using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class Reaction
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int RecipeId { get; set; }
        public ReactionType ReactionType { get; set; }
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Recipe Recipe { get; set; }
    }
}
