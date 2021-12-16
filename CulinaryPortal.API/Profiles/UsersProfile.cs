using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {     
            CreateMap<Entities.User, Models.UserDto>();
            CreateMap<Models.UserDto, Entities.User>();
        }
    }
}
