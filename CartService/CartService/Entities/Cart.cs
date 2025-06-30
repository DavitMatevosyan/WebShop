namespace CartService.Entities;

public class Cart : BaseEntity
{
    public List<Item> Items { get; set; } = [];
}