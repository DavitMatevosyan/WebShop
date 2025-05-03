using CartService.Application.Entities;

namespace CartService.Application.Repositories.Contracts;

public interface ICartRepository : IBaseRepository<Entities.Cart>
{
    Task<bool> AddItemToCart(Entities.Cart cart, Item item);
}