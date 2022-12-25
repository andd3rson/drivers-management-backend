using Drivers_Management.Domain.Models;


namespace Drivers_Management.Domain.Contracts.Repository
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Task<Vehicle> GetByPlateAsync(string plate);
        Task<IEnumerable<Vehicle>> GetAllAsync(int take, int skip);
    }
}