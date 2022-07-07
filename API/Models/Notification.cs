using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
    }
}
