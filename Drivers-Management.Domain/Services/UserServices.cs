using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Drivers_Management.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Drivers_Management.Domain.Services
{
    public class UserServices
    {
        private readonly UserManager<User> _user;

        public UserServices(UserManager<User> user)
        {
            _user = user;
        }

        public async Task<IdentityResult> Register(User user)
        {

            return await _user.CreateAsync(user, user.PasswordHash);

        }


        public Task<string> GenerateToken(User user)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("@!!!!8890132shalalla@124");
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.FirstName),
                    //  new Claim(ClaimTypes.Role, user.Roles.ToString())
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