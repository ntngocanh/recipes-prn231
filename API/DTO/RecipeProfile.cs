using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDTO>().ForMember(vm => vm.Popularity, m => m.MapFrom(u => RecipeUtils.CalcPopularity(u)));
            CreateMap<User, UserDTO>();
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<Step, StepDTO>();
        }
    }

    public class RecipeUtils
    {
        static RecipeDbContext _context = new RecipeDbContext();
        public static int CalcPopularity(Recipe recipe)
        {
            
            int sum = 0;
            sum += _context.Recipes.Include(x => x.Reactions).FirstOrDefault(x => x.RecipeId == recipe.RecipeId).Reactions.Count;
            if (recipe.User.RoleId == 3)
            {
                sum += 10;
            }
            return sum;
        }
    }
}
