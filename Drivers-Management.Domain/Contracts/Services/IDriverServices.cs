using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Utils;
using OneOf;

namespace Drivers_Management.Domain.Contracts.Services
{
    public interface IDriverServices
    {
        Task<IEnumerable<Driver>> GetAllAsync(int pageNumber, int pageSize);
        Task<Driver> GetByCpfAsync(string cpf);
        Task<OneOf<DomainExceptions, int>> AddAsync(Driver driver);
        Task<bool> UpdateAsync(Driver driver);
        Task<bool> Vinculate(int driverId, int vehicleId);
    }
}