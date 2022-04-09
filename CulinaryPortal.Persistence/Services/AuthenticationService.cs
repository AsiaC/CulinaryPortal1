using CulinaryPortal.Application.Identity;
using CulinaryPortal.Application.Models;
using CulinaryPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Persistence.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly SymmetricSecurityKey _symmetricSecurityKey;

        public AuthenticationService(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public async Task<UserDto> AuthenticateAsync(LoginDto request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                throw new Exception("401", new Exception($"User with {request.Username} not found."));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                throw new Exception("401", new Exception($"Credentials for '{request.Username} aren't valid'."));
            }

            var response = new UserDto
            {
                Username = user.UserName,
                Token = await CreateToken(user),
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return response;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto request)
        {

            var existingUser = await _userManager.FindByNameAsync(request.Username);

            if (existingUser != null)
            {
                throw new Exception($"Username '{request.Username}' already exists.");
            }

            var user = new User
            {
                UserName = request.Username.ToLower(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };


            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    throw new Exception($"{result.Errors}");
                }
                else
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "Member");
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception($"{result.Errors}");
                    }
                    else
                    {
                        return new UserDto
                        {
                            Username = user.UserName,
                            Token = await CreateToken(user),
                            Id = user.Id,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        };
                    }
                }
            }
            else
            {
                throw new Exception($"Email {request.Email } already exists.");
            }
        }

        public async Task<string> CreateToken(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
