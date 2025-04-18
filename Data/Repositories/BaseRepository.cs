using Data.Contexts;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> _table;
    protected readonly AppDbContext _context;

    protected BaseRepository(AppDbContext context)
    {
        _context = context; 
        _table = _context.Set<TEntity>();
    }
        
    public virtual async Task<RepositoryResult<TEntity>> AddAsync(TEntity entity)
    {
        try
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
            return ResultSuccess<TEntity>(201, entity);
        }
        catch (Exception ex)
        {
            return ResultFailure<TEntity>(500, $"Oväntat fel {ex}");
        }
    }
    public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await _table.ToListAsync();
            return ResultSuccess<IEnumerable<TEntity>>(200, entities);
        }
        catch (Exception ex)
        {
            return ResultFailure<IEnumerable<TEntity>>(500, $"Oväntat fel {ex}");
        }

    }

    public virtual async Task<RepositoryResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> findBy)
    {
        try
        {
            var entity = await _table.FirstOrDefaultAsync(findBy);
            if (entity == null)
            {
                return ResultFailure<TEntity>(404, "Entity hittades inte");
            }
            return ResultSuccess<TEntity>(200, entity);
        }
        catch (Exception ex)
        {
            return ResultFailure<TEntity>(500, $"Oväntat fel {ex}");
        }
    }

    public virtual async Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity,bool>> findBy)
    {
        try
        {
            var exists = await _table.AnyAsync(findBy);
            return ResultSuccess<bool>(200, exists);
        }
        catch (Exception ex)
        {
            return ResultFailure<bool>(500, $"Oväntat fel {ex.Message}");
        }
    }

    public virtual async Task<RepositoryResult<TEntity>> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            return ResultFailure<TEntity>(400, "Entity is null");

        try
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return ResultSuccess<TEntity>(200, entity);
        }
        catch (Exception ex)
        {
            return ResultFailure<TEntity>(500, $"Oväntat fel {ex}");
        }
    }

    public virtual async Task<RepositoryResult<TEntity>> DeleteAsync(TEntity entity)
    {
        if (entity == null)
            return ResultFailure<TEntity>(400, "Entity är null");
        
        try
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
            return ResultSuccess<TEntity>(200, entity);
        }
        catch (Exception ex)
        {
            return ResultFailure<TEntity>(500, $"Oväntat fel {ex}");
        }
    }





    private static RepositoryResult<T> ResultSuccess<T>(int statusCode, T data)
    {
        return new RepositoryResult<T>
        {
            Succeeded = true,
            StatusCode = statusCode,
            Data = data
        };
    }

    private static RepositoryResult<T> ResultFailure<T>(int statusCode, string error)
    {
        return new RepositoryResult<T>
        {
            Succeeded = false,
            StatusCode = statusCode,
            Error = error
        };
    }
}
