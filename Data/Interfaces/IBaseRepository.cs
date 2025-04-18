using Data.Models;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<RepositoryResult<TEntity>> AddAsync(TEntity entity);
        Task<RepositoryResult<TEntity>> DeleteAsync(TEntity entity);
        Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> findBy);
        Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<RepositoryResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> findBy);
        Task<RepositoryResult<TEntity>> UpdateAsync(TEntity entity);
    }
}