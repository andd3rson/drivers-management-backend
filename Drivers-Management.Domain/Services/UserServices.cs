using Drivers_Management.Domain.Models;
using Microsoft.AspNetCore.Identity;

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

        public async Task<IdentityResult> Register(User user)
        {

            user.EmailConfirmed = true;
            var res = await _user.CreateAsync(user, user.PasswordHash);
            if (!res.Succeeded)
            {
                return null;
            }
            return res;

        }
        public async Task<User> SignIn(User user)
        {
            var login = await _userManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, true);
            if (!login.Succeeded)
                return null;
            return user;
        }


    }
}