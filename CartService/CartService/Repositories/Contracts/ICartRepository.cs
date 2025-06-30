using CartService.Dtos;
using CartService.Entities;

namespace CartService.Repositories.Contracts;

public interface ICartRepository : IBaseRepository<Cart>
{
    Task<bool> AddItemToCart(Cart cart, Item item);
    Task UpdateItemsAsync(List<int> cartIds, UpdateItemDefinitionDto item);
    Task<List<Cart>> GetCartsByItemId(Guid itemId);
}