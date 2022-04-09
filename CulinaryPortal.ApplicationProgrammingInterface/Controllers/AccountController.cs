using CulinaryPortal.Application.Identity;
using CulinaryPortal.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.ApplicationProgrammingInterface.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto request)
        {
            try
            {
                var objectToReturn = await _authenticationService.RegisterAsync(request);                
                return Ok(objectToReturn);
            }
            catch (Exception e)
            {
                if (e.Message == "400")
                    return StatusCode(StatusCodes.Status400BadRequest, e.InnerException.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto request)
        {
            try
            {
                return Ok(await _authenticationService.AuthenticateAsync(request));
            }
            catch (Exception e)
            {                
                if(e.Message == "401")
                    return StatusCode(StatusCodes.Status401Unauthorized, e.InnerException.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, e);                 
            }
        }
    }
}
