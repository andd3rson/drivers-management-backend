using Drivers_Management.Domain.Models;

namespace Drivers_Management.Domain.Contracts.Services
{
    public interface IVehicleServices
    {
        Task<Vehicle> GetByPlateAsync(string plate);
        Task<IEnumerable<Vehicle>> GetAllAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Vehicle>> GetByAdvancedFilterAsync();
        Task<(Vehicle, bool)> CreateAsync(Vehicle vehicle);
        Task<bool> UpdateAsync(Vehicle vehicle);

        Task<Vehicle> GetByIdAsync(int id);
    }
}