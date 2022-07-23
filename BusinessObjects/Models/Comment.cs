using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public int? ParentCommentId { get; set; }
        [ForeignKey("ParentCommentId")]
        public virtual Comment ParentComment { get; set; }
        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
        public string Text { get; set; }
        
        public CommentStatus CommentStatus { get; set; }
        [JsonIgnore]
        public virtual ICollection<Report> Reports { get; set; }
    }
}
