using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Profiles
{
    public class MeasureProfile: Profile
    {
        public MeasureProfile()
        {
            CreateMap<Entities.Cookbook, Models.CookbookDto>();
        }
    }
}
