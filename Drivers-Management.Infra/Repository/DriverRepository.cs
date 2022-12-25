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
            string match = $"%{cpf}%";
            return await _context.Set<Driver>()
                            .Where(x => EF.Functions.Like(x.Cpf, match)).FirstOrDefaultAsync();
        }

        public async Task<bool> VinculateAsync(DriverVehicle driverVehicle)
        {
            _context.Set<DriverVehicle>().Add(driverVehicle);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync(int pageSize, int pageNumber)
        {
            return await _context.Set<Driver>()
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .Include(x => x.Vehicles)
                                 .ThenInclude(x => x.Vehicles)
                                 .ToListAsync();
        }
    }
}