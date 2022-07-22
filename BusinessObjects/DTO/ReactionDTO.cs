using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class ReactionDTO
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public ReactionType ReactionType { get; set; }
        public virtual UserDTO User { get; set; }
    }
}
