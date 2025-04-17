namespace CartService.Application.Dtos;

public class ItemDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Image { get; set; }
    public decimal Money { get; set; }
}