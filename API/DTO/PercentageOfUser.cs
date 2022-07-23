using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class PercentageOfUser
    {
        public string Role { get; set; }
        public float Percentage { get; set; }

        public PercentageOfUser(string role, float percentage)
        {
            Role = role;
            Percentage = percentage;
        }
    }
}
