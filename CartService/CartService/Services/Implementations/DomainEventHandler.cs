using CartService.DomainEvents;
using CartService.Dtos;
using CartService.Services.Contracts;

namespace CartService.Services.Implementations;

public class DomainEventHandler(ICartService cartService) : IDomainEventHandler
{
	public async Task HandleAsync(BaseDomainEvent domainEvent)
	{
		var eventResult = await ConvertDomainEventAndSendAsync(domainEvent);
		
		if (!eventResult.Success) throw new ApplicationException("Unhandled Exception");
	}

	private async Task<DomainEventResult> ConvertDomainEventAndSendAsync(BaseDomainEvent requestDto)
	{
		var eventResult = requestDto.EventType switch
		{
			EventType.ProductNameChangedEvent => await HandleProductNameChangedEventAsync(requestDto as ProductNameChangedEvent ?? throw new ApplicationException("Invalid Cast")),
			EventType.ProductPriceChangedEvent => await HandleProductPriceChangedEventAsync(requestDto as ProductPriceChangedEvent ?? throw new ApplicationException("Invalid Cast")),
			_ => throw new NotImplementedException("The event type is not implemented.")
		};

		return eventResult;
	}
	
	private async Task<DomainEventResult> HandleProductNameChangedEventAsync(ProductNameChangedEvent nameChangedEvent)
	{
		var updateItem = new UpdateItemDefinitionDto(
			nameChangedEvent.Id, 
			Name: nameChangedEvent.Name);

		await cartService.UpdateItemDefinitionAsync(updateItem);

		return new DomainEventResult(true);
	}
	
	private async Task<DomainEventResult> HandleProductPriceChangedEventAsync(ProductPriceChangedEvent priceChangedEvent)
	{
		var updateItem = new UpdateItemDefinitionDto(
			priceChangedEvent.Id, 
			Money: priceChangedEvent.Price);

		await cartService.UpdateItemDefinitionAsync(updateItem);

		return new DomainEventResult(true);
	}
}