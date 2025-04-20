using Catalog.Application.Products.Commands;
using Catalog.Domain.Contracts;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Catalog.Application.Tests.Products.Commands;

public class DeleteProductCommandTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly DeleteProductCommandHandler _handler;

    public DeleteProductCommandTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new DeleteProductCommandHandler(_productRepositoryMock.Object);
    }
    
    // and for failure cases
    [Fact]
    public async Task Handle_ExistingProduct_ShouldDelete()
    {
        // Arrange
        var product = new Product("Test", "Desc", "img.png", Guid.NewGuid(), new Money(10), 5);
        var productId = product.Id;

        _productRepositoryMock
            .Setup(repo => repo.GetAsync(productId))
            .ReturnsAsync(product);

        var command = new DeleteProductCommand(productId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(productId);
        product.IsDeleted.Should().BeTrue();
        _productRepositoryMock.Verify(repo => repo.UpdateAsync(product), Times.Once);
    }
}