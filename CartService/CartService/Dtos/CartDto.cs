namespace CartService.Dtos;

public class CartDto
{
    public int Id { get; set; }
    public List<ItemDto> Items { get; set; } = [];
}