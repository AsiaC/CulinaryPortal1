using AutoMapper;
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
            CreateMap<Entities.Cookbook, Models.CookbookDto>();
        }
    }
}
