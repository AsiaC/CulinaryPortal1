using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper, ITokenService tokenService)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken"); 
            }

            using var hmac = new HMACSHA512();

            var user = new User
            {
                Username = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                IsActive = true,
                Email = registerDto.Email
            };

            _culinaryPortalRepository.AddUser(user);
            await _culinaryPortalRepository.SaveChangesAsync();

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _culinaryPortalRepository.GetUserAsync(loginDto.Username);

            if (user == null)
                return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid password");
                }
            }
            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        //metodapomocnicza do spr czy user o takiej nazwie uzytkownika istnieje
        private async Task<bool> UserExists(string username)
        {
            return await _culinaryPortalRepository.UserExists(username);
        }
    }
}
