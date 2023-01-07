using AutoMapper;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drivers_Management.Application.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _users;
        private readonly IMapper _mapper;

        public UserController(UserServices users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest user)
        {
            var result = await _users.Register(_mapper.Map<User>(user));
            if (!result.Item1.Succeeded)
                return BadRequest();


            return Ok(new
            {
                token = result.Item2
            });
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] UserLogInRequest user)
        {
            
            var t = _mapper.Map<User>(user);
            var result = await _users.SignIn(t);
            if (result.Item2 == null)
                return BadRequest();


            return Ok(new
            {
                token = result.Item2
            });
        }

    }
}