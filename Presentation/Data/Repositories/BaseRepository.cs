using Microsoft.EntityFrameworkCore;
using Presentation.Data.Contexts;
using Presentation.Models;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Presentation.Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }


    public virtual async Task<RepositoryResult> CreateAsync(TEntity entity)
    {
        try
        {

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new RepositoryResult
            {
                Succeeded = true,

            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult
            {
                Succeeded = false,
                StatusCode = 500,
                Error = $"Error occurred while creating {nameof(TEntity)} entity : {ex.Message}"
            };

        }
    }

    public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAll()
    {
        try
        {
            var entities = await _dbSet.ToListAsync();


            return new RepositoryResult<IEnumerable<TEntity>>
            {
                Succeeded = true,
                Result = entities
            };

        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<TEntity>>
            {
                Succeeded = false,
                Error = $"Error occurred: {ex.Message}"
            };

        }
    }

    public virtual async Task<RepositoryResult<TEntity?>> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _dbSet.FirstOrDefaultAsync(predicate);

            if (entity == null)
            {
                return new RepositoryResult<TEntity?>
                {
                    Succeeded = false,
                    StatusCode = 404,
                    Error = $"{nameof(TEntity)} not found"
                };
            }
            return new RepositoryResult<TEntity?>
            {
                Succeeded = true,
                Result = entity
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<TEntity?>
            {
                Succeeded = false,
                Error = $"Error occurred: {ex.Message}"
            };

        }

    }

    public virtual async Task<RepositoryResult> AlreadyExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {

        var result = await _dbSet.AnyAsync(predicate);

        return result
           ? new RepositoryResult { Succeeded = true }
           : new RepositoryResult { Succeeded = false };
    }



    public virtual async Task<RepositoryResult> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return new RepositoryResult
            {
                Succeeded = true,

            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult
            {
                Succeeded = false,
                StatusCode = 500,
                Error = $"Error occurred while updating {nameof(TEntity)} entity : {ex.Message}"
            };

        }
    }

    public virtual async Task<RepositoryResult> DeleteAsync(TEntity entity)
    {
        try
        {

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return new RepositoryResult
            {
                Succeeded = true,

            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult
            {
                Succeeded = false,
                StatusCode = 500,
                Error = $"Error occurred while removing {nameof(TEntity)} entity : {ex.Message}"
            };

        }
    }


}

