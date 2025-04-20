using Catalog.Application.Products.Queries;
using Catalog.Domain.Contracts;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Catalog.Application.Tests.Products.Queries;

// also for failure cases
public class GetProductQueryTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly GetProductQueryHandler _handler;

    public GetProductQueryTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _handler = new GetProductQueryHandler(_productRepositoryMock.Object);
    }
    
    [Fact]
    public async Task Handle_ProductExists_ShouldReturn()
    {
        // Arrange
        var category = new Category("Test Category", "cat-img.png", null);
        var categoryId = category.Id;

        var product = new Product("Test", "Description", "img.png", categoryId, new Money(100), 10);
        product.Category = category;
        var productId = product.Id;

        _productRepositoryMock
            .Setup(r => r.GetProductWithCategoryAsync(productId))
            .ReturnsAsync(product);

        var query = new GetProductQuery(productId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(productId);
        result.Name.Should().Be("Test");
        result.Category.Should().NotBeNull();
        result.Category.Id.Should().Be(categoryId);
        _productRepositoryMock.Verify(r => r.GetProductWithCategoryAsync(productId), Times.Once);
    }
}