using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Models;
using Drivers_Management.Infra.Context;

namespace Drivers_Management.Infra.Repository
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DriverManagementDbContext context) : base(context)
        {
        }
    }
}