using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class NumberOfRecipePerDay
    {
        public DateTime Date { get; set; }
        public int NumberOfRecipe { get; set; }

        public NumberOfRecipePerDay(DateTime date, int numberOfRecipe)
        {
            Date = date;
            NumberOfRecipe = numberOfRecipe;
        }
    }
}
