using AutoMapper;
using CulinaryPortal.Application.Features.Cookbooks.Commands.CreateCookbook;
using CulinaryPortal.Application.Features.Cookbooks.Commands.UpdateCookbook;
using CulinaryPortal.Application.Features.Rates.Commands.CreateRate;
using CulinaryPortal.Application.Features.Recipes.Commands.CreateRecipe;
using CulinaryPortal.Application.Features.Recipes.Commands.UpdateRecipe;
using CulinaryPortal.Application.Features.ShoppingLists.Commands.CreateShoppingList;
using CulinaryPortal.Application.Features.ShoppingLists.Commands.UpdateShoppingList;
using CulinaryPortal.Application.Features.Users.Commands.UpdateUser;
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
            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<Cookbook, CookbookDto>()
                .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
            CreateMap<CookbookDto, Cookbook>();
            CreateMap<CreateCookbookCommand, Cookbook>().ReverseMap();
            //CreateMap<UpdateCookbookCommand, Cookbook>()
            //    .ForMember(
            //    dest => dest.CookbookRecipes,
            //    opt => opt.MapFrom(src => src.CookbookRecipes));

            CreateMap<CookbookRecipeDto, CookbookRecipe>()
                .ForMember(
                dest => dest.Recipe,
                opt => opt.MapFrom(src => src.Recipe))
                .ReverseMap();
            CreateMap<UpdateCookbookCommand, CookbookRecipe>().ReverseMap();

            CreateMap<IngredientDto, Ingredient>().ReverseMap();

            CreateMap<Instruction, InstructionDto>().ReverseMap();

            CreateMap<ListItemDto, ListItem>()
               .ForMember(
               dest => dest.Name,
               opt => opt.MapFrom(src => src.ItemName));
            CreateMap<ListItem, ListItemDto>()
                .ForMember(
                dest => dest.ItemName,
                opt => opt.MapFrom(src => src.Name));

            CreateMap<MeasureDto, Measure>().ReverseMap();

            CreateMap<Photo, PhotoDto>();

            CreateMap<RateDto, Rate>().ReverseMap();
            CreateMap<CreateRateCommand, Rate>().ReverseMap();            

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
                opt => opt.MapFrom(src => src.Rates.Count() == 0 ? 0 : Math.Round(( (decimal)src.Rates.Sum(x => x.Value) / (decimal)src.Rates.Count() ),2) ));
            CreateMap<RecipeDto, Recipe>()
               .ForMember(
               dest => dest.Category,
               opt => opt.MapFrom(src => src.Category));

            CreateMap<CreateRecipeCommand, Recipe>().ReverseMap();
            CreateMap<UpdateRecipeCommand, Recipe>().ReverseMap();

            CreateMap<RecipeIngredientDto, RecipeIngredient>().ReverseMap();              

            CreateMap<ShoppingListDto, ShoppingList>()
                .ForMember(
                dest => dest.Items,
                opt => opt.MapFrom(src => src.Items));
            CreateMap<ShoppingList, ShoppingListDto>()
                .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));

            CreateMap<CreateShoppingListCommand, ShoppingList>().ReverseMap();//TODO CZY POTRZEBUJE?
            //.ForMember(
            //dest => dest.Items,
            //opt => opt.MapFrom(src => src.Items));
            CreateMap<UpdateShoppingListCommand, ShoppingList>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UpdateUserCommand, User>().ReverseMap();
        }
    }
}
