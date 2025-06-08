using System.Text;
using System.Text.Json;
using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Configuration;
using RabbitMQ.Client;

namespace Catalog.Infrastructure.Services;

public class RabbitMqService : INotificationPublisher, IAsyncDisposable
{
    private IConnection? connection;
    private IChannel? channel;
    private readonly RabbitMqConfiguration options;
    private readonly ConnectionFactory factory;

    public RabbitMqService(RabbitMqConfiguration options)
    {
        this.options = options;
        factory = new ConnectionFactory()
        {
            HostName = options.Hostname,
            Port = options.Port,
            UserName = "davo", // get from configs
            Password = "password"
        };
    }
    
    public async Task InitializeAsync()
    {
        connection = await factory.CreateConnectionAsync();
        channel = await connection.CreateChannelAsync();
        await EnsureConfiguration();
    }
    
    public async Task Publish<T>(string exchange, T message, CancellationToken cancellationToken = default)
    {
        if (channel == null)
            throw new NullReferenceException("Channel is null");
        
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        await channel.BasicPublishAsync(
            exchange: exchange,
            routingKey: options.RoutingKey,
            mandatory: true,
            basicProperties: new BasicProperties(),
            body: body,
            cancellationToken: cancellationToken);
    }

    private async Task EnsureConfiguration()
    {
        foreach (var configuration in options.Configuration)
        {
            await channel.ExchangeDeclareAsync(
                exchange: configuration.Exchange,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false);
            
            foreach (var queue in configuration.Queues)
            {
                await channel.QueueDeclareAsync(
                    queue: queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                await channel.QueueBindAsync(
                    queue: queue,
                    exchange: configuration.Exchange,
                    routingKey: options.RoutingKey);
            }
        }
    }
    
    public async ValueTask DisposeAsync()
    {
        if (connection != null)
            await connection.DisposeAsync();
    
        if (channel != null)
            await channel.DisposeAsync();
    }
}