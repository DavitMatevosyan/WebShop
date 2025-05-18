using Catalog.Api.Endpoints;
using Catalog.Api.Extensions;
using Catalog.Api.HostedServices;
using Catalog.Application.Categories.Commands;
using Catalog.Application.Categories.Queries;
using Catalog.Application.Interfaces;
using Catalog.Application.Products.Commands;
using Catalog.Application.Products.Queries;
using Catalog.Domain.DomainEvents;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Configuration;
using Catalog.Infrastructure.Events;
using Catalog.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,
        typeof(BaseEndpoint).Assembly,
        typeof(AddCategoryCommand).Assembly,
        typeof(GetCategoryQuery).Assembly,
        typeof(AddProductCommand).Assembly,
        typeof(GetProductQuery).Assembly);

    config.RegisterServicesFromAssemblyContaining<IDomainEvent>();
});

builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

builder.Services.AddRepositoryServices()
    .AddCategoryServices()
    .AddProductServices();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

builder.Services.Configure<RabbitMqConfiguration>(
    builder.Configuration.GetSection("RabbitMqConfiguration"));

builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IOptions<RabbitMqConfiguration>>().Value;
    return new RabbitMqService(config);
});

builder.Services.AddSingleton<INotificationPublisher>(sp => sp.GetRequiredService<RabbitMqService>());

var app = builder.Build();

var rabbitMq = new RabbitMqInitializer(app.Services.GetRequiredService<RabbitMqService>());
await rabbitMq.StartAsync(CancellationToken.None);




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

    app.MapGroup("/api/Categories").MapCategoriesEndpoints(builder.Configuration);
    app.MapGroup("/api/Products").MapProductsEndpoints(builder.Configuration); 

app.Run();