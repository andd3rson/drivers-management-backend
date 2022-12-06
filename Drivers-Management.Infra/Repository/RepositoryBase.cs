using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Models;

namespace Drivers_Management.Infra.Repository
{
    public abstract class RepositoryBase<T> : IBaseRepository<T> where T : BaseModel
    {
        public Task<int> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}