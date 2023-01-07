namespace Drivers_Management.Application.Controllers
{
    public record UserRegisterRequest(string Email, string Password, string FirstName, string LastName);
    public record UserLogInRequest(string Email, string Password);
}