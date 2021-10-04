using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Profiles
{
    public class CookbookProfile: Profile
    {
        public CookbookProfile()
        {
            //CreateMap<Entities.Cookbook, Models.CookbookDto>()
            //    .ForMember(
            //    dest => dest.Recipes,
            //    opt => opt.MapFrom(src => src.CookbookRecipes.Select(x => x.Recipe)));
            CreateMap<Models.CookbookRecipeDto, Entities.CookbookRecipe>();
            CreateMap<Entities.CookbookRecipe, Models.CookbookRecipeDto>();

            /*
            CreateMap<Entities.CookbookRecipe, Models.RecipeDto>()
                .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.RecipeId));

            CreateMap<Models.CookbookDto, Entities.Cookbook>();
            CreateMap<Entities.Cookbook, Models.CookbookDto>()
                .ForMember(
                dest => dest.Recipes,
                opt => opt.MapFrom(src => src.CookbookRecipes.Select(x => x.Recipe).ToList()));
            */

            /*
            CreateMap<Entities.Cookbook, Models.CookbookDto>()
                .ForMember(d => d.Recipes, opt => opt.MapFrom(s => s.CookbookRecipes.Select(x => x.Recipe)));
            //.ForMember(d => d.Recipes, opt => opt.MapFrom(s => s.CookbookRecipes.Select(x => x.Recipe).ToList()));

            CreateMap<Entities.CookbookRecipe, Models.RecipeDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.RecipeId));
            */
            /*
            CreateMap<Entities.Cookbook, Models.CookbookDto>();

            CreateMap<Entities.CookbookRecipe, Models.RecipeDto>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.RecipeId));

            CreateMap<Models.CookbookDto, Entities.Cookbook>()
                  .AfterMap((s, d) =>
                  {
                      foreach (var studentImage in d.CookbookRecipes)
                          studentImage.CookbookId = s.Id;
                  });

            CreateMap<Models.RecipeDto, Entities.CookbookRecipe>()
                  .ForMember(d => d.RecipeId, opt => opt.MapFrom(s => s.Id));
            */
            /*
            CreateMap<Entities.Cookbook, Models.CookbookDto>()
                .ForMember(
                dest => dest.Recipes,
                opt => opt.MapFrom(src => src.CookbookRecipes.Select(x => x.RecipeId)));
            */

            //CreateMap<Entities.Cookbook, Models.CookbookDto>();


            CreateMap<Models.RecipeIngredientDto, Entities.RecipeIngredient>();
            CreateMap<Entities.RecipeIngredient, Models.RecipeIngredientDto>();

            CreateMap<Models.MeasureDto, Entities.Measure>();
            CreateMap<Entities.Measure, Models.MeasureDto>();
            CreateMap<Models.IngredientDto, Entities.Ingredient>();
            CreateMap<Entities.Ingredient, Models.IngredientDto>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<Models.ShoppingListDto, Entities.ShoppingList>();
            CreateMap<Entities.ShoppingList, Models.ShoppingListDto>()
                .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
            
            CreateMap<Models.ListItemDto, Entities.ListItem>();
            CreateMap<Entities.ListItem, Models.ListItemDto>();

            //opt => opt.MapFrom(src => src.CookbookRecipes.Select(x => x.Recipe).Where(s=>s.Id == RecipeId)));

            //.Include<Entities.Recipe, Models.RecipeDto>()

            //opt => opt.MapFrom(x => Mapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeDto>>(x.CookbookRecipes.Select(y => y.Recipe).ToList())));

            //opt => opt.MapFrom(x => x.GoodsAndProviders.Select(y => y.Providers).ToList()));
            //opt => opt.MapFrom(x => x.TagLinks.Select(y => y.Tag.Name).ToList()));

            //CreateMap<Character, GetCharacterDto>().ForMember
            //(dto => dto.Skills, c => c.MapFrom(c => c.CharacterSkills.Select(cs => cs.Skill)));

            //.ForMember(dto => dto.Companies,
            //opt => opt.MapFrom(x => Mapper.Map<IList<Company>, IList<CompanyDTO>>(x.UserCompanies.Select(y => y.Company).ToList())));


            //var configuration = new MapperConfiguration(c => {
            //    c.CreateMap<ParentSource, ParentDestination>()
            //         .Include<ChildSource, ChildDestination>();
            //    c.CreateMap<ChildSource, ChildDestination>();
            //});
        }
    }
}
