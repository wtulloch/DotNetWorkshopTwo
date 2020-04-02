using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Api.Extensions;
using Ticketing.Data;
using Ticketing.Data.Repositories;
using Ticketing.Models.Dtos;

namespace Ticketing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUserAysnc(int id)
        {
            var userDto = await _userRepository.GetUserByIdAsync(id);
            
            return userDto;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(RegisterUserDto newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            try
            {
               var user = await _userRepository.RegisterAsync(newUser);

               return CreatedAtRoute("GetUser", new {id = user.Id}, user);
            }
            catch (UserExistsException uex)
            {
                //log here
                var errorResponse = new
                {
                    Error = new
                    {
                        Message = $"User {newUser.Email} is already registered"
                    }
                };

                return BadRequest(errorResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}