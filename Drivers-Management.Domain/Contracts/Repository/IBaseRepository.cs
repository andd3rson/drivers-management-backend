using Drivers_Management.Domain.Models;

namespace Drivers_Management.Domain.Contracts.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseModel
    {        

        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> Create(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);

    }
}