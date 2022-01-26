using DotNetCoreWebAPIWith_JWT.Model;
using DotNetCoreWebAPIWith_JWT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebAPIWith_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private IAuthenticateService _authenticateService;
        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost("ValidateAuthorize")]
        public IActionResult ValidateAuthorizeAsync([FromBody] UserDTO user)
        {
            var userAuthenticate = _authenticateService.Authenticate(user.UserName, user.Password);

            if (userAuthenticate == null)
                return BadRequest(new { message = "User Credential is wrong!!!" });

            return Ok(userAuthenticate);
        }
    }
}
