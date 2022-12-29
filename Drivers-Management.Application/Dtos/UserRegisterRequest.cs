namespace Drivers_Management.Application.Controllers
{
    public record UserRegisterRequest(string UserName, string Email, string Password, string FirstName, string LastName);
}