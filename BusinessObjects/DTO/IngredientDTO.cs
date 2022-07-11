using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class IngredientDTO
    {
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
        public string Text { get; set; }
    }
}
