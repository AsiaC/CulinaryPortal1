﻿using AutoMapper;
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
            CreateMap<Entities.Cookbook, Models.CookbookDto>()

                .ForMember(
                dest => dest.Recipes,
                opt => opt.MapFrom(src => src.CookbookRecipes.Select(x => x.Recipe)));


            CreateMap<Models.CookbookDto, Entities.Cookbook>();




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