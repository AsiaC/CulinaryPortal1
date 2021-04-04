using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Entities.Recipe, Models.RecipeDto>()
                .ForMember(
                dest => dest.Author,
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(
                dest => dest.Category,
                opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(
                dest=>dest.Preparation,
                opt => opt.MapFrom(src=>src.PreparationTime))
                .ForMember(
                dest => dest.Difficulty,
                opt => opt.MapFrom(src => src.DifficultyLevel));  // CookbookRecipes.Select(x => x.Cookbook)));
                
            //CreateMap<Entities.User, Models.UserDto>()
            //    .ForMember(
            //    dest => dest.Name,
            //    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
                

            CreateMap<Models.RecipeDto, Entities.Recipe>();


        }
    }
}
