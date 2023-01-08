using Drivers_Management.Domain.Models;

namespace Drivers_Management.Domain.Contracts.Services
{
    public interface IDriverServices
    {
        Task<IEnumerable<Driver>> GetAllAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Driver>> GetByCpfAsync(string cpf);
        Task<(Driver, bool)> CreateAsync(Driver driver);
        Task<bool> UpdateAsync(Driver driver);
        Task<bool> Vinculate(int driverId, int vehicleId);
    }
}