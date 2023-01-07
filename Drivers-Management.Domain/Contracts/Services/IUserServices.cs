using Drivers_Management.Domain.Models;
using Microsoft.AspNetCore.Identity;

public interface IUserServices
{

    Task<(IdentityResult, string)> Register(User user);
    Task<(User, string)> SignIn(User user);
}
