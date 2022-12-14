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
    }
}