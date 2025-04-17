using CartService.Application.Entities;
using CartService.Application.Repositories.Contracts;
using LiteDB;

namespace CartService.Application.Repositories.Implementations;

public class CartRepository(string connectionString) : BaseRepository<Entities.Cart>(connectionString), ICartRepository
{
    private readonly string _connectionString = connectionString;

    public Task<bool> AddItemToCart(Entities.Cart cart, Item item)
    {
        using var dbConn = new LiteDatabase(_connectionString);

        cart.Items.Add(item);
        
        dbConn.GetCollection<Entities.Cart>().Update(cart.Id, cart);
        
        return Task.FromResult(true);
        
    }
}