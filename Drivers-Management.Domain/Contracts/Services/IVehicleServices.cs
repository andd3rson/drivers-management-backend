using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Utils;
using OneOf;

namespace Drivers_Management.Domain.Contracts.Services
{
    public interface IVehicleServices
    {
        Task<Vehicle> GetByPlateAsync(string plate);
        Task<IEnumerable<Vehicle>> GetAllAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Vehicle>> GetByAdvancedFilterAsync();
        Task<OneOf<DomainExceptions, int>> PostAsyncllAsync(Vehicle vehicle);
        Task<bool> UpdateAsync(Vehicle vehicle);
        
        Task<Vehicle> GetByIdAsync(int id);
    }
}