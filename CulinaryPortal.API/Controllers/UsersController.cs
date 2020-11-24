using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return Ok(_mapper.Map<IEnumerable<UserDto>>(usersFromRepo));
        }
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        //{
        //    var usersFromRepo = await _culinaryPortalRepository.GetUsersAsync();
        //    return Ok(usersFromRepo);
        //}

        [Authorize]
        [HttpGet("{userId}", Name ="GetUser")]
        public ActionResult<UserDto> GetUser(int userId)
        {
            var checkIfUserExists = _culinaryPortalRepository.UserExists(userId);
            if (!checkIfUserExists)
            {
                return NotFound();
            }
            var userFromRepo = _culinaryPortalRepository.GetUser(userId);
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
        public ActionResult DeleteUser(int userId)
        {
            var userFromRepo = _culinaryPortalRepository.GetUser(userId);
            if (userFromRepo == null)
            {
                return NotFound();
            }
            _culinaryPortalRepository.DeleteUser(userFromRepo);
            _culinaryPortalRepository.Save();
            return NoContent();
        }
    }
}
