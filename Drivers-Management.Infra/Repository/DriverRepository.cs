using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Models;
using Drivers_Management.Infra.Context;

namespace Drivers_Management.Infra.Repository
{
    public class DriverRepository : RepositoryBase<Driver>, IDriverRepository
    {
        public DriverRepository(DriverManagementDbContext context) : base(context)
        {

        }

        public async Task<Driver> GetByCpfAsync(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}