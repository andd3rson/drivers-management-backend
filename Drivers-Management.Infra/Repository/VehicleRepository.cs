using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Utils;
using Drivers_Management.Infra.Context;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Drivers_Management.Infra.Repository
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        private readonly DriverManagementDbContext _context;
        public VehicleRepository(DriverManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetByPlateAsync(string plate)
        {
            return await _context.Set<Vehicle>()
                .Where(x => EF.Functions.Like(x.Plate, $"${plate}")).FirstOrDefaultAsync();
        }
    }
}