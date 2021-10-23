using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [AllowAnonymous]
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
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            try
            {//TODO do sprawdzenia pztrz na  37.using dtos - tam sprawdzam unikalnosc username dla usera 
                var userFromRepo = await _culinaryPortalRepository.GetUserAsync(registerDto.Username);
                if (userFromRepo != null)
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

                await _culinaryPortalRepository.AddUserAsync(user);

                return new UserDto
                {
                    Username = user.Username,
                    Token = _tokenService.CreateToken(user)
                };
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            try
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

                var userToReturn = new UserDto 
                {
                    Username = user.Username,
                    Token = _tokenService.CreateToken(user),
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };

                return userToReturn;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }
                
    }
}
