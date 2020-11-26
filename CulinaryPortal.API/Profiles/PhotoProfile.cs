using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Profiles
{
    public class PhotoProfile: Profile
    {
        public PhotoProfile()
        {
            CreateMap<Entities.Photo, Models.PhotoDto>();
        }
    }
}
