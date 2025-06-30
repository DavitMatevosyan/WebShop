using CartService.Dtos;
using CartService.Entities;
using CartService.Repositories.Contracts;
using CartService.Services.Contracts;

namespace CartService.Services.Implementations;

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
                Items = cart.Items.Select(item => 
                    new ItemDto(item.Id, item.Name, item.Image ?? string.Empty, item.Money, item.CartId))
                    .ToList()
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
                Name = item.Name,
                CartId = cart.Id
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
                Name = item.Name,
                CartId = item.CartId
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

    public async Task UpdateItemDefinitionAsync(UpdateItemDefinitionDto item)
    {
        try
        {
            var cartIds = (await cartRepository
                .GetCartsByItemId(item.Id))
                .Select(cart => cart.Id)
                .ToList();

            if (cartIds.Any())
                await cartRepository.UpdateItemsAsync(cartIds, item);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}