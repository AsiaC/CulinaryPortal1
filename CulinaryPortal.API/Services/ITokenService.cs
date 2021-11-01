using CulinaryPortal.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
