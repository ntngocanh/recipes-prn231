using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class FeaturedTag
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeaturedTagId { get; set; }
        public string Name { get; set; }
    }
}
