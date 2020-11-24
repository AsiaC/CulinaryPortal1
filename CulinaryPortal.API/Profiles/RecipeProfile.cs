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
            CreateMap<Entities.Recipe, Models.RecipeDto>();
        }
    }
}
