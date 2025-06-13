namespace CartService.Application.Dtos;

public record ItemDto(Guid Id, string Name, string Image, decimal Money, int CartId);
public record UpdateItemDefinitionDto(Guid Id, string? Name = null, string? Image = null, decimal? Money = null);