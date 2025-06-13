using CartService.Application.Dtos;
using CartService.Application.Entities;
using CartService.Application.Repositories.Contracts;
using LiteDB;

namespace CartService.Application.Repositories.Implementations;

public class CartRepository(string connectionString) : BaseRepository<Cart>(connectionString), ICartRepository
{
    private readonly string _connectionString = connectionString;

    public Task<bool> AddItemToCart(Cart cart, Item item)
    {
        using var dbConn = new LiteDatabase(_connectionString);

        cart.Items.Add(item);
        
        dbConn.GetCollection<Cart>().Update(cart.Id, cart);
        
        return Task.FromResult(true);
        
    }

    public Task UpdateItemsAsync(List<int> cartIds, UpdateItemDefinitionDto item)
    {
        using var dbConn = new LiteDatabase(_connectionString);

        var carts = dbConn
            .GetCollection<Cart>()
            .Query()
            .Where(cart => cartIds.Contains(cart.Id) && cart.Items.Select(i => i.Id).Contains(item.Id))
            .ToList();
            
        var existingItems = carts     
            .SelectMany(cart => cart.Items.Where(i => i.Id == item.Id))
            .ToList();

        foreach (var existingItem in existingItems)
        {
            if(!string.IsNullOrEmpty(item.Name))
                existingItem.Name = item.Name;
            
            if(!string.IsNullOrEmpty(item.Image))
                existingItem.Image = item.Image;
            
            if(item.Money != default)
                existingItem.Money = item.Money.Value;
        }

        dbConn.GetCollection<Cart>().Update(carts);
        
        return Task.CompletedTask;
    }

    public Task<List<Cart>> GetCartsByItemId(Guid itemId)
    {
        using var dbConn = new LiteDatabase(_connectionString);

        var carts = dbConn
            .GetCollection<Cart>()
            .Query()
            .Where(cart => cart.Items.Select(x => x.Id).Contains(itemId))
            .ToList();
        
        return Task.FromResult(carts);
    }
}