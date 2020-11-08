using CulinaryPortal.API.Entities;
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
    public class UsersController :ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;

        public UsersController(ICulinaryPortalRepository culinaryPortalRepository)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
        }

        [HttpGet]
        //public ActionResult<IEnumerable<User>> GetUsers()
        //{
        //    var users = _culinaryPortalRepository.GetUsers();
        //    return users; 
        //}

        public IActionResult GetUsers()
        {
            var authorsFromRepo = _culinaryPortalRepository.GetUsers();
            return new JsonResult(authorsFromRepo);
        }
    }
}
