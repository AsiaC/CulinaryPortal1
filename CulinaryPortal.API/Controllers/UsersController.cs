using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
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
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var usersFromRepo = _culinaryPortalRepository.GetUsers();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(usersFromRepo));
        }


    }
}
