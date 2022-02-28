using AutoMapper;
using CulinaryPortal.Application.Models;
using CulinaryPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();

            CreateMap<Cookbook, CookbookDto>()
                .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
            CreateMap<CookbookDto, Cookbook>();

            CreateMap<CookbookRecipeDto, CookbookRecipe>();
            CreateMap<CookbookRecipe, CookbookRecipeDto>();

            CreateMap<IngredientDto, Ingredient>();
            CreateMap<Ingredient, IngredientDto>();

            CreateMap<Instruction, InstructionDto>();
            CreateMap<InstructionDto, Instruction>();

            CreateMap<ListItemDto, ListItem>()
               .ForMember(
               dest => dest.Name,
               opt => opt.MapFrom(src => src.ItemName));
            CreateMap<ListItem, ListItemDto>()
                .ForMember(
                dest => dest.ItemName,
                opt => opt.MapFrom(src => src.Name));

            CreateMap<MeasureDto, Measure>();
            CreateMap<Measure, MeasureDto>();

            CreateMap<Photo, PhotoDto>();

            CreateMap<RateDto, Rate>();
            CreateMap<Rate, RateDto>();

            CreateMap<Recipe, RecipeDto>()
                .ForMember(
                dest => dest.Author,
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(
                dest => dest.Category,
                opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(
                dest => dest.CountRates,
                opt => opt.MapFrom(src => src.Rates.Count()))
                .ForMember(
                dest => dest.RateValues,
                opt => opt.MapFrom(src => src.Rates.Sum(x => x.Value)))
                .ForMember(dest => dest.TotalScore,
                opt => opt.MapFrom(src => src.Rates.Count() == 0 ? 0 : (src.Rates.Sum(x => x.Value) / src.Rates.Count())));
            CreateMap<RecipeDto, Recipe>()
               .ForMember(
               dest => dest.Category,
               opt => opt.MapFrom(src => src.Category));

            CreateMap<RecipeIngredientDto, RecipeIngredient>();
            CreateMap<RecipeIngredient, RecipeIngredientDto>();                                

            CreateMap<ShoppingListDto, ShoppingList>()
                .ForMember(
                dest => dest.Items,
                opt => opt.MapFrom(src => src.Items));
            CreateMap<ShoppingList, ShoppingListDto>()
                .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));      
                        
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            
            CreateMap<UserUpdateDto, User>();
        }
    }
}
