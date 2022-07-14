using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class RecipeViewModel
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name="Thời gian nấu")]
        public string Duration { get; set; }
        public IFormFile Image { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<IngredientDTO> Ingredients { get; set; }
        public virtual ICollection<StepDTO> Steps { get; set; }
    }
}
