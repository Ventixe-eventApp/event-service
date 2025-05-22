using Microsoft.EntityFrameworkCore;
using Presentation.Data.Contexts;
using Presentation.Data.Entities;
using Presentation.Models;
using System.Linq.Expressions;

namespace Presentation.Data.Repositories;

public class EventRepository(DataContext context) : BaseRepository<EventEntity>(context), IEventRepository
{
    public override async Task<RepositoryResult<IEnumerable<EventEntity>>> GetAll()
    {
        try
        {
            var entities = await _dbSet.Include(e => e.Packages).ToListAsync();
            return new RepositoryResult<IEnumerable<EventEntity>>
            {
                Succeeded = true,
                Result = entities
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<EventEntity>>
            {
                Succeeded = false,
                Error = $"Error occurred: {ex.Message}"
            };
        }
    }

    public override async Task<RepositoryResult<EventEntity?>> GetAsync(Expression<Func<EventEntity, bool>> predicate)
    {
        try
        {
            var entity = await _dbSet.Include(x => x.Packages).FirstOrDefaultAsync(predicate);

            if (entity == null)
            {
                return new RepositoryResult<EventEntity?>
                {
                    Succeeded = false,
                    StatusCode = 404,
                    Error = $"{nameof(EventEntity)} not found"
                };
            }
            return new RepositoryResult<EventEntity?>
            {
                Succeeded = true,
                Result = entity
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<EventEntity?>
            {
                Succeeded = false,
                Error = $"Error occurred: {ex.Message}"
            };

        }
    }
}

