using Catalog.Infrastructure.Services;

namespace Catalog.Api.HostedServices;

public class RabbitMqInitializer : IHostedService
{
    private readonly RabbitMqService _rabbitMqService;

    public RabbitMqInitializer(RabbitMqService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _rabbitMqService.InitializeAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}