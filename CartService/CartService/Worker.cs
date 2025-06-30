using System.Text;
using System.Text.Json;
using CartService.DomainEvents;
using CartService.Services.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CartService;

public class Worker : BackgroundService
{
    private IConnection? connection;
    private IChannel? channel;

    private IDomainEventHandler domainEventHandler;

    public Worker(IDomainEventHandler domainEventHandler)
    {
        this.domainEventHandler = domainEventHandler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            // just some passwords for the rabbitmq, of course this MUST be moved to KV/Secrets or some other secure storage
            HostName = "localhost",
            Port = 5672,
            UserName = "davo",
            Password = "password"
        };
        connection = await factory.CreateConnectionAsync(stoppingToken);
        channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await channel.QueueDeclareAsync(
            "domain_event_queue",
            true,
            false,
            false,
            cancellationToken: stoppingToken);
        
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            
            await ResolveMessageTypeAndInvokeAsync(message);
        };

        await channel.BasicConsumeAsync(queue: "domain_event_queue",
            autoAck: true,
            consumer: consumer, 
            stoppingToken);

        Console.WriteLine("Listening. Press [enter] to exit.");
    }

    private async Task ResolveMessageTypeAndInvokeAsync(string message)
    {
        var requestDto =  JsonSerializer.Deserialize<BaseDomainEvent>(message)
            ?? throw new ApplicationException($"Could not deserialize {message}");

        await domainEventHandler.HandleAsync(requestDto); 
    }

    public override async void Dispose()
    {
        if(channel is not null)
            await channel?.CloseAsync()!;
        
        if(connection is not null)
            await connection?.CloseAsync()!;
        
        base.Dispose();
    }
}