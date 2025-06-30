using CartService.Entities;

namespace CartService.Repositories.Contracts;

public interface IBaseRepository<T> where T: BaseEntity
{
    Task<List<T>> GetAsync(int id);
    Task AddAsync(T entity);
    Task RemoveAsync(int entityId);
}