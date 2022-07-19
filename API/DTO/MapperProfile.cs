using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Recipe, RecipeDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<Step, StepDTO>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentRequest, Comment>();

        }
    }
}
