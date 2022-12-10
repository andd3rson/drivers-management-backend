using Drivers_Management.Domain.Models;

namespace Drivers_Management.Domain.Contracts.Services
{
    public interface IDriverServices
    {
        Task<IEnumerable<Driver>> GetAllAsync();
        Task<Driver> GetByCpfAsync(int cpf);
        Task<Guid> AddAsync(Driver driver);
    }
}