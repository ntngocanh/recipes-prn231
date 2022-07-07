using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
        public string Text { get; set; }
        public CommentStatus CommentStatus { get; set; }
    }
}
