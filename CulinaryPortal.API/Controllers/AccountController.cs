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

        public AccountController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto registerDto)
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
            return user;
        }

        //metodapomocnicza do spr czy user o takiej nazwie uzytkownika istnieje
        private async Task<bool> UserExists(string username)
        {
            return await _culinaryPortalRepository.UserExists(username);
        }
    }
}
