using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int ParentCommentId { get; set; }
        public string Text { get; set; }
        public CommentStatus CommentStatus { get; set; }
        public virtual UserDTO User { get; set; }

        public int NumberOfReplies { get; set; }
    }
}
