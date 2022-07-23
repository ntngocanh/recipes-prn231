using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class RecipeDTO
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Image { get; set; }
        public DateTime DateCreated { get; set; }
        public RecipeStatus RecipeStatus { get; set; }
        public int UserId { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual ICollection<IngredientDTO> Ingredients { get; set; }
        public virtual ICollection<StepDTO> Steps { get; set; }
        public int Popularity { get; set; }
        //public virtual ICollection<Reaction> Reactions { get; set; }
        //public virtual ICollection<Comment> Comments { get; set; }
    }
}
