using Catalog.Api.Endpoints.Category;
using Catalog.Api.Endpoints.Product;
using Catalog.Domain.Contracts;
using Catalog.Infrastructure.Repositories;

namespace Catalog.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCategoryServices(this IServiceCollection services)
        => services.AddScoped<AddCategoryEndpoint>()
                   .AddScoped<GetCategoriesEndpoint>()
                   .AddScoped<GetCategoryEndpoint>()
                   .AddScoped<UpdateCategoryEndpoint>()
                   .AddScoped<DeleteCategoryEndpoint>();
    
    
    public static IServiceCollection AddProductServices(this IServiceCollection services)
        => services.AddScoped<GetProductsEndpoint>()
                   .AddScoped<AddProductEndpoint>()
                   .AddScoped<UpdateProductEndpoint>()
                   .AddScoped<DeleteProductEndpoint>();


    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        => services.AddScoped<ICategoryRepository, CategoryRepository>()
                   .AddScoped<IProductRepository, ProductRepository>();


}