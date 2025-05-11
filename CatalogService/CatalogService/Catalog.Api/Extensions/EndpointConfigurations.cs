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
        
        group.MapGet("/{id:guid}", async (GetCategoryEndpoint handler, [FromRoute] Guid id) => await handler.HandleAsync(id));
        group.MapGet("/", async (GetCategoriesEndpoint handler, 
            [FromQuery] string? searchText, 
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = default) => await handler.HandleAsync(searchText, 
                                                                            page, 
                                                                            pageSize == default ? defaultPageSize : pageSize));
        
        group.MapPost("/", async (AddCategoryEndpoint handler, [FromBody] AddCategoryDto dto) => await handler.HandleAsync(dto));
        
        group.MapPut("/", async (UpdateCategoryEndpoint handler, [FromBody] UpdateCategoryDto dto) => await handler.HandleAsync(dto));
        
        group.MapDelete("/{id:guid}", async (DeleteCategoryEndpoint handler, [FromQuery] Guid id) => await handler.HandleAsync(id));
    }

    public static void MapProductsEndpoints(this RouteGroupBuilder group, IConfiguration config)
    {
        int defaultPageSize = config.GetValue<int>("DefaultPageSize");
        
        group.MapGet("/", async (GetProductsEndpoint handler, 
            [FromQuery] Guid categoryId, 
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = default) => await handler.HandleAsync(categoryId, 
                page, 
                pageSize == default ? defaultPageSize : pageSize));
        
        group.MapPost("/", async (AddProductEndpoint handler, [FromBody] AddProductDto dto) => await handler.HandleAsync(dto));
        
        group.MapPut("/", async (UpdateProductEndpoint handler, [FromBody] UpdateProductDto dto) => await handler.HandleAsync(dto));
        
        group.MapDelete("/{id:guid}", async (DeleteProductEndpoint handler, [FromQuery] Guid id) => await handler.HandleAsync(id));
    }
}