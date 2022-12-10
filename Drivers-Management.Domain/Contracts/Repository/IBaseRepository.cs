using Drivers_Management.Domain.Models;

namespace Drivers_Management.Domain.Contracts.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseModel
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> Create(TEntity entity);
        Task<bool> Update(TEntity entity);

    }
}