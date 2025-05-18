using Catalog.Infrastructure.Services;

namespace Catalog.Api.HostedServices;

public class RabbitMqInitializer : IHostedService
{
    private readonly RabbitMqService rabbitMqService;

    public RabbitMqInitializer(RabbitMqService rabbitMqService)
    {
        this.rabbitMqService = rabbitMqService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await rabbitMqService.InitializeAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}