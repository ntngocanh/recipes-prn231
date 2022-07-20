using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class CommentRequest
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public int ParentCommentId { get; set; }
        public string Text { get; set; }
        public CommentStatus CommentStatus { get; set; }
    }
}
