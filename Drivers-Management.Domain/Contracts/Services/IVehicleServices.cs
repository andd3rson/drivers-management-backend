using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Utils;
using OneOf;

namespace Drivers_Management.Domain.Contracts.Services
{
    public interface IVehicleServices
    {
        Task<Vehicle> GetByPlateAsync(string plate);
        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task<IEnumerable<Vehicle>> GetByAdvancedFilterAsync();
        Task<OneOf<DomainExceptions, Vehicle>> PostAsyncllAsync();
        Task<bool> UpdateAsync(Vehicle vehicle);
    }
}