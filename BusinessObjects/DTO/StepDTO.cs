using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class StepDTO
    {
        public int StepId { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
    }
}
