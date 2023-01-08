using Drivers_Management.Domain.Models;
using Microsoft.AspNetCore.Identity;

public interface IUserServices
{

    Task<IdentityResult> Register(User user);
    Task<User> SignIn(User user);
}
