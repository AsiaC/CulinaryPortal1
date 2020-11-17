using CulinaryPortal.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
