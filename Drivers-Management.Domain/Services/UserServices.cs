using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Drivers_Management.Application;
using Drivers_Management.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Drivers_Management.Domain.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _user;
        private readonly SignInManager<User> _userManager;
        public UserServices(UserManager<User> user, SignInManager<User> userManager)
        {
            _user = user;
            _userManager = userManager;
        }

        public async Task<(IdentityResult, string)> Register(User user)
        {

            user.EmailConfirmed = true;
            var res = await _user.CreateAsync(user, user.PasswordHash);
            if (!res.Succeeded)
            {
                return (res, null);
            }
            var token = await GenerateToken(user);

            return (res, token);

        }
        public async Task<(User, string)> SignIn(User user)
        {
            var login = await _userManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, true);
            if (!login.Succeeded)
                return (user, null);
            return (user, await GenerateToken(user));
        }



        private Task<string> GenerateToken(User user)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var s = Settings.Secret;
            var key = Encoding.ASCII.GetBytes(s);
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