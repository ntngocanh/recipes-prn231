using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Step
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StepId { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
