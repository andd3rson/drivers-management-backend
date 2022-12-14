using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Utils;
using OneOf;

namespace Drivers_Management.Domain.Services
{
    public class VehicleServices : IVehicleServices
    {
        public Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vehicle>> GetByAdvancedFilterAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle> GetByPlateAsync(string plate)
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<DomainExceptions, Vehicle>> PostAsyncllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}