using Drivers_Management.Domain.Models;

namespace Drivers_Management.Tests.Fakers
{
    public class DriversFakers
    {
        public static List<Driver> listDrivers()
        {
            return new List<Driver>()
            {
                new Driver(){
                    Id = 1,
                    Cpf = "",
                    Email = "",
                    Name = "",
                    CreatedAt = DateTime.UtcNow
                },
                new Driver(){
                    Id = 2,
                    Cpf = "",
                    Email = "",
                    Name = "",
                    CreatedAt = DateTime.UtcNow
                },
                new Driver(){
                    Id = 3,
                    Cpf = "",
                    Email = "",
                    Name = "",
                    CreatedAt = DateTime.UtcNow
                },
                new Driver(){
                    Id = 4,
                    Cpf = "",
                    Email = "",
                    Name = "",
                    CreatedAt = DateTime.UtcNow
                },
                new Driver(){
                    Id = 5,
                    Cpf = "",
                    Email = "",
                    Name = "",
                    CreatedAt = DateTime.UtcNow
                },
                new Driver(){
                    Id = 6,
                    Cpf = "",
                    Email = "",
                    Name = "",
                    CreatedAt = DateTime.UtcNow
                },
                new Driver(){
                    Id = 7,
                    Cpf = "",
                    Email = "",
                    Name = "",
                    CreatedAt = DateTime.UtcNow
                },
                new Driver(){
                    Id = 8,
                    Cpf = "",
                    Email = "",
                    Name = "",
                    CreatedAt = DateTime.UtcNow
                },
                new Driver(){
                    Id = 9,
                    Cpf = "",
                    Email = "",
                    Name = "",
                    CreatedAt = DateTime.UtcNow
                },new Driver(){
                    Id = 10,
                    Cpf = "",
                    Email = "",
                    Name = "",
                    CreatedAt = DateTime.UtcNow
                },
                new Driver(){
                    Id = 11,
                    Cpf = "",
                    Email = "",
                    Name = "",
                    CreatedAt = DateTime.UtcNow
                },
                new Driver(){
                    Id = 12,
                    Cpf = "",
                    Email = "",
                    Name = "",
                    CreatedAt = DateTime.UtcNow
                }

            };
        }

        public static Driver TakeOneDriver()
        {
            return new Driver()
            {

                Cpf = "84512154086",
                Email = "test@test.com",
                Name = "TestSon"
            };
        }
    }
}