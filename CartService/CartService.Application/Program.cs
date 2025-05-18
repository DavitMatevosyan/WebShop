using CartService.Application.Repositories.Contracts;
using CartService.Application.Repositories.Implementations;
using CartService.Application.Services.Contracts;
using CartService.Application.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CartService.Application;

public static class Program
{
    public static void Main(string[] args)
    {
        Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddScoped<ICartRepository, CartRepository>(x => new CartRepository("./CartServiceDatabase.dll"));
                
                services.AddScoped<ICartService, Services.Implementations.CartService>();
                services.AddScoped<IDomainEventHandler, DomainEventHandler>();
                
                services.AddHostedService<Worker>();
            })
            .Build()
            .Run();
    }
}