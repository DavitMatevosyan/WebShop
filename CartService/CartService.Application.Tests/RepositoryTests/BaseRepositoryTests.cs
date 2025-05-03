using LiteDB;
using CartService.Application.Repositories.Implementations;

namespace CartService.Application.Tests.RepositoryTests;

public class BaseRepositoryTests
{
    private void DisposeDb(LiteDatabase db)
    {
        db.GetCollection<FakeEntity>().DeleteAll();
    }
    
    [Fact]
    public async Task AddAsync_Should_Insert()
    {
        // arrange
        var repo = new BaseRepository<FakeEntity>(Constants.FakeDbConnectionString);
        using var db = new LiteDatabase(Constants.FakeDbConnectionString);
        DisposeDb(db);

        var entity = new FakeEntity()
        {
            Id = 1
        };

        // act
        await repo.AddAsync(entity);

        // assert
        var fetched = db.GetCollection<FakeEntity>().Find(x => x.Id == 1);

        Assert.NotNull(fetched);
    }
    
    [Fact]
    public async Task GetAsync_Should_ReturnEntity()
    {
        // arrange
        var entity = new FakeEntity()
        {
            Id = 1
        };
        
        using var db = new LiteDatabase(Constants.FakeDbConnectionString);
        DisposeDb(db);
        db.GetCollection<FakeEntity>().Insert(entity);
        
        var repo = new BaseRepository<FakeEntity>(Constants.FakeDbConnectionString);
        
        // act
        var result = await repo.GetAsync(1);

        // assert
        Assert.Single(result);
        Assert.Equal(result.First().Id, entity.Id);
    }
    
    [Fact]
    public async Task RemoveAsync_Should_Delete()
    {
        // arrange
        var repo = new BaseRepository<FakeEntity>(Constants.FakeDbConnectionString);
        using var db = new LiteDatabase(Constants.FakeDbConnectionString);
        DisposeDb(db);
        
        var entity = new FakeEntity()
        {
            Id = 1
        };
        
        await repo.AddAsync(entity);

        // act
        await repo.RemoveAsync(1);
        
        // assert
        var deleted = db.GetCollection<FakeEntity>().Find(x => x.Id == 1).ToList();
        Assert.Empty(deleted);
    }
}