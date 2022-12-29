using Drivers_Management.Domain.Models;
using Microsoft.AspNetCore.Identity;

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
    }
}