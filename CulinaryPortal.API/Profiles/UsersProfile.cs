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
            CreateMap<Entities.User, Models.UserDto>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));            
        }
    }
}
