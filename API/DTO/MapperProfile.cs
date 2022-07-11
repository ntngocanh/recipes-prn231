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
        }
    }
}
