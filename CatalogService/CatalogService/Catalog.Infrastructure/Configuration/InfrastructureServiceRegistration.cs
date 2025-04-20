using Catalog.Application.Interfaces;
using Catalog.Domain.Contracts;
using Catalog.Infrastructure.Events;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.Configuration ;

    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // to be replaced by retrieving from configuration
                // local db connection
                options.UseSqlServer("Server=localhost,1433; Database=AM; TrustServerCertificate=True; User=sa; Password=DavSQL123@");
                // options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); 

                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped<ICategoryRepository, CategoryRepository>();

                services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            });
            
            return services;
        }
    }