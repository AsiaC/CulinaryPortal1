using CulinaryPortal.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Identity
{
    public interface IAuthenticationService
    {
        Task<UserDto> AuthenticateAsync(LoginDto request);
        Task<UserDto> RegisterAsync(RegisterDto request);
    }
}
