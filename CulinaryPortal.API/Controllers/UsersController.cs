﻿using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;

        public UsersController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //[HttpGet]
        //public IActionResult GetUsers()
        //{
        //    var authorsFromRepo = _culinaryPortalRepository.GetUsers();
        //    return new JsonResult(authorsFromRepo);
        //}
        [AllowAnonymous ]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var usersFromRepo = await _culinaryPortalRepository.GetUsersAsync();
            var users = _mapper.Map<IEnumerable<UserDto>>(usersFromRepo);
            return Ok(users);
        }
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        //{
        //    var usersFromRepo = await _culinaryPortalRepository.GetUsersAsync();
        //    return Ok(usersFromRepo);
        //}

        //[Authorize] to poźniej dodam
        [HttpGet("{userId}", Name ="GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            var checkIfUserExists = await _culinaryPortalRepository.UserExistsAsync(userId);
            if (!checkIfUserExists)
            {
                return NotFound();
            }
            var userFromRepo =await _culinaryPortalRepository.GetUserAsync(userId);
            return Ok(_mapper.Map<UserDto>(userFromRepo));
        }

        [HttpPost]
        public ActionResult<UserDto> CreateUser(User user)
        {
            _culinaryPortalRepository.AddUser(user);
            _culinaryPortalRepository.Save();

            var userToReturn = _mapper.Map<UserDto>(user);
            return CreatedAtAction("GetUser", new { userId = userToReturn.Id }, userToReturn);
        }

        [HttpDelete("userId")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var userFromRepo =await _culinaryPortalRepository.GetUserAsync(userId);
            if (userFromRepo == null)
            {
                return NotFound();
            }
            _culinaryPortalRepository.DeleteUser(userFromRepo);
            _culinaryPortalRepository.Save();
            return NoContent();
        }

        // GET: api/users/3/cookbook
        [HttpGet("{userId}/cookbook", Name = "GetUserCookbook")]
        public async Task<IActionResult> GetUserCookbookAsync(int userId)
        {
            var cookbookFromRepo = await _culinaryPortalRepository.GetUserCookbookAsync(userId);
            if (cookbookFromRepo == null)
            {
                return NotFound();
            }

            var cookbook = _mapper.Map<Models.CookbookDto>(cookbookFromRepo);
            return Ok(cookbook);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var user = await _culinaryPortalRepository.GetUserAsync(userUpdateDto.Id);

            _mapper.Map(userUpdateDto, user);

            _culinaryPortalRepository.UpdateUser(user);

            if (await _culinaryPortalRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update user");
        }

        // GET: api/users/3/recipes
        [HttpGet("{userId}/recipes", Name = "GetUserRecipes")]
        public async Task<IActionResult> GetUserRecipesAsync(int userId)
        {
            var recipesFromRepo = await _culinaryPortalRepository.GetUserRecipesAsync(userId);
            if (recipesFromRepo == null)
            {
                return NotFound();
            }

            var userRecipes = _mapper.Map<IEnumerable<RecipeDto>>(recipesFromRepo);
            return Ok(userRecipes);
        }

    }
}
