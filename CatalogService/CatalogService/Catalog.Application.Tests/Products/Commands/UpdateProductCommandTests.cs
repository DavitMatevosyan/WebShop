using Catalog.Application.Products.Commands;
using Catalog.Domain.Contracts;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Catalog.Application.Tests.Products.Commands;

public class UpdateProductCommandTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly UpdateProductCommandHandler _handler;
    
    private readonly Guid existingCategoryId = Guid.NewGuid();
    
    public UpdateProductCommandTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _handler = new UpdateProductCommandHandler(_productRepositoryMock.Object, _categoryRepositoryMock.Object);
    }
    
    // similar to this add other cases
    [Fact]
    public async Task Handle_WithValidProduct_ShouldUpdateProduct()
    {
        // Arrange
        _categoryRepositoryMock.Setup(repo => repo.GetAsync(existingCategoryId))
            .ReturnsAsync(new Category("Category", "Image uri", null));

        var product = new Product(
            "Name",
            null,
            null, 
            existingCategoryId,
            new Money(10),
            5);
        var productId = product.Id;
        
        _productRepositoryMock.Setup(repo => repo.GetAsync(productId))
            .ReturnsAsync(product);
        
        var command = new UpdateProductCommand(
            Id: productId,
            Name: "Updated Name");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBe(Guid.Empty);
        
        product.Name.Should().Be("Updated Name");
        
        _categoryRepositoryMock.VerifyNoOtherCalls();
    }
}