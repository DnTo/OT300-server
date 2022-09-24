using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;
        public AuthController(IAuthBusiness authBusiness)
        {
            _authBusiness = authBusiness;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLogin Login)
        {
            string Token = _authBusiness.Login(Login);

            if (!string.IsNullOrEmpty(Token))
                return Ok(Token);

            return Unauthorized("{ok:false}");
        }
    }
}