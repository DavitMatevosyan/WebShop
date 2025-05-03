using CartService.Application.Entities;
using CartService.Application.Repositories.Contracts;
using LiteDB;

namespace CartService.Application.Repositories.Implementations;

// connection string should be retrieved from appsettings/secrets/KeyVault
public class BaseRepository<T>(string connectionString) : IBaseRepository<T> where T : BaseEntity
{
    public Task<List<T>> GetAsync(int id)
    {
        using var dbConn = new LiteDatabase(connectionString);
        
        var data = dbConn
            .GetCollection<T>()
            .Query()
            .Where(e => e.Id == id)
            .ToList();

        return Task.FromResult(data);
    }

    public Task AddAsync(T entity)
    {
        using var dbConn = new LiteDatabase(connectionString);
        
        dbConn.GetCollection<T>().Insert(entity);
        
        return Task.CompletedTask;
    }

    public Task RemoveAsync(int entityId)
    {
        using var dbConn = new LiteDatabase(connectionString);
        
        dbConn.GetCollection<T>().DeleteMany(x => x.Id == entityId);
        
        return Task.CompletedTask;    
    }
}