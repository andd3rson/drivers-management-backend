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
        private readonly UserServices _users;
        private readonly IMapper _mapper;

        public UserController(UserServices users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest user)
        {
            var result = await _users.Register(_mapper.Map<User>(user));
            if(!result.Succeeded)
                return BadRequest();
                
            return Ok(result);
        }
    }
}