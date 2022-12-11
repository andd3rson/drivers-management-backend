using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Utils;
using OneOf;

namespace Drivers_Management.Domain.Contracts.Services
{
    public interface IDriverServices
    {
        Task<IEnumerable<Driver>> GetAllAsync();
        Task<Driver> GetByCpfAsync(int cpf);
        Task<OneOf<DomainExceptions, Guid>> AddAsync(Driver driver);
    }
}