using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Api.Extensions;
using Ticketing.Models.Dtos;

namespace Ticketing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterUserDto newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            return new OkResult();
        }
    }
}