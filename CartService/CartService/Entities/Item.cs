namespace CartService.Entities;

public class Item
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Image { get; set; }
    public decimal Money { get; set; }
    
    public int CartId { get; set; }
}