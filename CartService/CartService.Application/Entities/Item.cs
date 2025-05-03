namespace CartService.Application.Entities;

public class Item
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Image { get; set; }
    public decimal Money { get; set; }
}