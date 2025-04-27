using System.Linq.Expressions;
using Catalog.Domain.Entities;

namespace Catalog.Domain.Contracts;

public interface IBaseRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Adds an entity to db
    /// </summary>
    /// <param name="entity">the entity to add</param>
    /// <returns></returns>
    Task AddAsync(T entity);
    
    /// <summary>
    /// Gets an entity by id
    /// </summary>
    /// <param name="id">the id of the entity</param>
    /// <returns></returns>
    Task<T> GetAsync(Guid id);
    
    /// <summary>
    /// Gets the list of entities filtered by the given predicate 
    /// </summary>
    /// <param name="predicate">the predicate to filter with</param>
    /// <param name="pageNumber">the page of filtration</param>
    /// <param name="pageSize">the number of entities in a single page</param>
    /// <returns></returns>
    Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);
    
    /// <summary>
    /// Updates an entity
    /// </summary>
    /// <param name="entity">the entity to update</param>
    /// <returns></returns>
    Task UpdateAsync(T entity);
    
    /// <summary>
    /// HARD deletes an entity (prefer the use of soft deletion)
    /// </summary>
    /// <param name="id">the entity to delete</param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
}
