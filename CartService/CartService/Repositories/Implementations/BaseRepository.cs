using CartService.Entities;
using CartService.Repositories.Contracts;
using LiteDB;
using LiteDB.Engine;

namespace CartService.Repositories.Implementations;

// connection string should be retrieved from appsettings/secrets/KeyVault
public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly string _connectionString;

    public BaseRepository(string connectionString)
    {
        var liteEngine = new LiteEngine(new EngineSettings()
        {
            Filename = connectionString,
        });
        
        _connectionString = connectionString;

        // create new db if doesn't exist
        var database = new LiteDatabase(liteEngine);
    }

    public Task<List<T>> GetAsync(int id)
    {
        using var dbConn = new LiteDatabase(_connectionString);
        
        var data = dbConn
            .GetCollection<T>()
            .Query()
            .Where(e => e.Id == id)
            .ToList();

        return Task.FromResult(data);
    }

    public Task AddAsync(T entity)
    {
        using var dbConn = new LiteDatabase(_connectionString);
        
        dbConn.GetCollection<T>().Insert(entity);
        
        return Task.CompletedTask;
    }

    public Task RemoveAsync(int entityId)
    {
        using var dbConn = new LiteDatabase(_connectionString);
        
        dbConn.GetCollection<T>().DeleteMany(x => x.Id == entityId);
        
        return Task.CompletedTask;    
    }
}