using CartService.Application.Dtos;
using CartService.Application.Entities;
using CartService.Application.Repositories.Contracts;
using CartService.Application.Services.Contracts;

namespace CartService.Application.Services.Implementations;

public class CartService(ICartRepository cartRepository) : ICartService
{
    public async Task<List<CartDto>> GetCartItemsAsync(int cartId)
    {
        var result = await cartRepository.GetAsync(cartId);
        
        if(result is null)
            throw new ApplicationException($"No cart found with id {cartId}");

        return result.Select(cart => new CartDto
            {
                Id = cart.Id,
                items = cart.Items.Select(item => 
                    new ItemDto
                    {
                        Id = item.Id,
                        Image = item.Image,
                        Money = item.Money,
                        Name = item.Name
                    }).ToList()
            }).ToList();
    }

    public async Task<bool> AddToCartAsync(int cartId, ItemDto item)
    {
        try
        {
            // check for cart existence, if not exception will be thrown
            var cart = (await cartRepository.GetAsync(cartId)).First();

            var newItem = new Item()
            {
                Id = item.Id,
                Image = item.Image,
                Money = item.Money,
                Name = item.Name
            };
            
            await cartRepository.AddItemToCart(cart, newItem);
        }
        catch (InvalidOperationException)
        {
            // this will be replaced by a logger.
            Console.WriteLine("AddToCartAsync — Sequence contains no matching element.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

            return false;
        }
        
        return true;
    }

    public async Task RemoveFromCartAsync(int cartId, ItemDto item)
    {
        try
        {
            var cart = (await cartRepository.GetAsync(cartId)).First();

            var oldItem = new Item()
            {
                Id = item.Id,
                Image = item.Image,
                Money = item.Money,
                Name = item.Name
            };
            
            cart.Items.Remove(oldItem);
        }
        catch (InvalidOperationException)
        {
            // this should be replaced by a logger.
            Console.WriteLine("AddToCartAsync — Sequence contains no matching element.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}