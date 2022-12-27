using Drivers_Management.Domain.Models;

namespace Drivers_Management.Tests.Fakers
{
    public class VehiclesFakers
    {
        public static List<Vehicle> GetVehiclesList()
        {
            return new List<Vehicle>()
            {
                new Vehicle()
                {
                    Id = 1
                },
                new Vehicle()
                {
                    Id = 2
                },
                new Vehicle()
                {
                    Id = 3
                },
                new Vehicle()
                {
                    Id = 4
                },
                new Vehicle()
                {
                    Id = 5
                },
                new Vehicle()
                {
                    Id = 11
                },
                new Vehicle()
                {
                    Id = 12
                },
                new Vehicle()
                {
                    Id = 13
                },
                new Vehicle()
                {
                    Id = 14
                },
                new Vehicle()
                {
                    Id = 15
                }
            };
        }
    }
}