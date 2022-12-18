using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Models;
using Drivers_Management.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Drivers_Management.Infra.Repository
{
    public class DriverRepository : RepositoryBase<Driver>, IDriverRepository
    {

        private readonly DriverManagementDbContext _context;
        public DriverRepository(DriverManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Driver> GetByCpfAsync(string cpf)
        {
            return await _context.Set<Driver>()
                            .Where(x => EF.Functions.Like(x.Cpf, $"${cpf}")).FirstOrDefaultAsync();
        }

        public async Task<bool> VinculateAsync(string driverId, string vehicleId)
        {
            // _context.DriverVehicle.Add(new DriverVehicle { DriversId = Guid.Parse(driverId), VehiclesId = Guid.Parse(vehicleId) });
            // return await _context.SaveChangesAsync() > 0;
           return true;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync(int pageSize, int pageNumber)
        {
            return await _context.Set<Driver>()
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .Include(x => x.Vehicles)
                                 .ToListAsync();
        }
    }
}