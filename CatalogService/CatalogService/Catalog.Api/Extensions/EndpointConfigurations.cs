using Catalog.Api.Endpoints.Category;
using Catalog.Api.Endpoints.Product;
using Catalog.Application.Categories.Dtos;
using Catalog.Application.Products.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Extensions;

public static class EndpointConfigurations
{
    public static void MapCategoriesEndpoints(this RouteGroupBuilder group, IConfiguration config)
    {
        int defaultPageSize = config.GetValue<int>("DefaultPageSize");

        group.MapGet("/{id:guid}", async ([FromServices] GetCategoryEndpoint handler, [FromRoute] Guid id) => await handler.HandleAsync(id));
        
        group.MapGet("/", async ([FromServices] GetCategoriesEndpoint handler, 
            [FromQuery] string? searchText, 
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = default) => await handler.HandleAsync(searchText, 
                                                                            page, 
                                                                            pageSize == default ? defaultPageSize : pageSize));
        
        group.MapPost("/", async ([FromServices] AddCategoryEndpoint handler, [FromBody] AddCategoryDto dto) => await handler.HandleAsync(dto));
        
        group.MapPut("/", async ([FromServices] UpdateCategoryEndpoint handler, [FromBody] UpdateCategoryDto dto) => await handler.HandleAsync(dto));
        
        group.MapDelete("/{id:guid}", async ([FromServices] DeleteCategoryEndpoint handler, [FromRoute] Guid id) => await handler.HandleAsync(id));
    }

    public static void MapProductsEndpoints(this RouteGroupBuilder group, IConfiguration config)
    {
        int defaultPageSize = config.GetValue<int>("DefaultPageSize");
        
        group.MapGet("/", async ([FromServices] GetProductsEndpoint handler, 
            [FromQuery] Guid categoryId, 
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = default) => await handler.HandleAsync(categoryId, 
                page, 
                pageSize == default ? defaultPageSize : pageSize));
        
        group.MapPost("/", async ([FromServices] AddProductEndpoint handler, [FromBody] AddProductDto dto) => await handler.HandleAsync(dto));
        
        group.MapPut("/", async ([FromServices] UpdateProductEndpoint handler, [FromBody] UpdateProductDto dto) => await handler.HandleAsync(dto));
        
        group.MapDelete("/{id:guid}", async ([FromServices] DeleteProductEndpoint handler, [FromQuery] Guid id) => await handler.HandleAsync(id));
    }
}