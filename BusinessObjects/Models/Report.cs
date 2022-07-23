using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class Report
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }
        public int CommentId { get; set; }
        [ForeignKey("CommentId")]
       
        
        public virtual Comment Comment { get; set; }
        public string Text { get; set; }
    }
}
