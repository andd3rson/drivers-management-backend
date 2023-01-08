using Drivers_Management.Domain.Models;

namespace Drivers_Management.Domain.Contracts.Repository
{
    public interface IDriverRepository : IBaseRepository<Driver>
    {
        Task<IEnumerable<Driver>> GetByCpfAsync(string cpf);
        Task<bool> VinculateAsync(DriverVehicle driverVehicle);
        Task<IEnumerable<Driver>> GetAllAsync(int take, int skip);
    }
}