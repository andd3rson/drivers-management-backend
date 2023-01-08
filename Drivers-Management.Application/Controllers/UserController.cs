using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Drivers_Management.Application.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _users;
        private readonly IMapper _mapper;

        private readonly Settings _settings;
        public UserController(UserServices users, IMapper mapper, IOptions<Settings> options)
        {
            _settings = options.Value;
            _users = users;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest user)
        {
            var userMapped = _mapper.Map<User>(user);
            var result = await _users.Register(userMapped);
            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(x => x.Description));


            return Ok(new
            {
                token = await GenerateToken(userMapped)
            });
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] UserLogInRequest user)
        {
            var userMapped = _mapper.Map<User>(user);
            var result = await _users.SignIn(userMapped);
            if (result == null)
                return BadRequest();
            return Ok(new
            {
                token = await GenerateToken(userMapped)
            });
        }


        private Task<string> GenerateToken(User user)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_settings.SecretKey);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Email, user.Email),
                    // new Claim(ClaimTypes.Role, user.Roles.ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(120),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature.ToString())
            };

            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return Task.FromResult(jwtSecurityTokenHandler.WriteToken(securityToken));
        }

    }
}