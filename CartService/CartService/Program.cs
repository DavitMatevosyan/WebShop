using CartService;
using CartService.Repositories.Contracts;
using CartService.Repositories.Implementations;
using CartService.Services.Contracts;
using CartService.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.Authority = "http://keycloak:8080/realms/WebShop";
		options.Audience = "account";
		options.RequireHttpsMetadata = false;

		options.TokenValidationParameters = new()
		{
			ValidateIssuer = false,
			ValidIssuer = "http://keycloak:8080/realms/WebShop",

			ValidateAudience = true,
			ValidAudience = "account",

			ValidateLifetime = true
		};
	});

builder.Services.AddScoped<ICartRepository, CartRepository>(x => new CartRepository("./CartServiceDatabase.dll"));
            
builder.Services.AddScoped<ICartService, CartService.Services.Implementations.CartService>();
builder.Services.AddScoped<IDomainEventHandler, DomainEventHandler>();
            
builder.Services.AddHostedService<Worker>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();