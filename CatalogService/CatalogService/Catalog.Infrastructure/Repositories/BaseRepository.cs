using Catalog.Domain.Contracts;
using Catalog.Domain.Entities;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Catalog.Infrastructure.Repositories;

public class BaseRepository<T>(ApplicationDbContext dbContext) : IBaseRepository<T> where T : BaseEntity
{
    public async Task AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
    }

    public async Task<T> GetAsync(Guid id)
    {
        return await dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await dbContext.Set<T>().Where(predicate).ToListAsync();
    }

    public Task UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);
        
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetAsync(id);

        dbContext.Set<T>().Remove(entity);
    }
}