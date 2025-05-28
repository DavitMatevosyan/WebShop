using CartService.Application.Dtos;
using CartService.Application.Entities;

namespace CartService.Application.Repositories.Contracts;

public interface ICartRepository : IBaseRepository<Cart>
{
    Task<bool> AddItemToCart(Cart cart, Item item);
    Task UpdateItemsAsync(List<int> cartIds, UpdateItemDefinitionDto item);
    Task<List<Cart>> GetCartsByItemId(Guid itemId);
}