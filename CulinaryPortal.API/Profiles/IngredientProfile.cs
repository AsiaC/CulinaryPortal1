using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Profiles
{
    public class IngredientProfile: Profile
    {
        public IngredientProfile()
        {
            CreateMap<Entities.Ingredient, Models.IngredientDto>();
        }
    }
}
