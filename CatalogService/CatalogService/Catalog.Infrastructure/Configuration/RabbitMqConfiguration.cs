namespace Catalog.Infrastructure.Configuration;

public class RabbitMqConfiguration
{
    public string Hostname { get; set; } = null!;
    public int Port { get; set; }
    public string RoutingKey { get; set; } = null!;
    public List<RabbitMqConfigurationOptions> Configuration { get; set; } = [];   
}

public class RabbitMqConfigurationOptions
{
    public string Exchange { get; set; } = null!;
    public List<string> Queues { get; set; } = null!;
}