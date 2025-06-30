using CartService.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
	private readonly ICartService cartService;

	public CartController(ICartService cartService)
	{
		this.cartService = cartService;
	}

	[HttpGet]
	[Authorize]
	[Route("{id:int}")]
	public async Task<ActionResult<List<int>>> GetCartItemsAsync(int id)
	{
		var items = await cartService.GetCartItemsAsync(id);
		
		if(!items.Any())
			return NotFound($"No cart items found with id {id}");
		
		return Ok(items);
	}
}