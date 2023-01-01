using Microsoft.AspNetCore.Identity;

namespace Drivers_Management.Domain.Models
{
    public class User : IdentityUser
    {
        public User()
        {

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public ERole Roles { get; set; }
    }
}