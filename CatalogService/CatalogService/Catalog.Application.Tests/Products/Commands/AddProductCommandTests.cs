using Catalog.Application.Products.Commands;
using Catalog.Domain.Contracts;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using FluentAssertions;
using Moq;

namespace Catalog.Application.Tests.Products.Commands;

public class AddProductCommandTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly AddProductCommandHandler _handler;
    
    private readonly Guid existingCategoryId = Guid.NewGuid();
    
    public AddProductCommandTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _handler = new AddProductCommandHandler(_productRepositoryMock.Object, _categoryRepositoryMock.Object);
    }
    
    [Fact]
    public async Task Handle_WithValidProduct_ShouldAddProduct()
    {
        // Arrange
        _categoryRepositoryMock.Setup(repo => repo.GetAsync(existingCategoryId))
            .ReturnsAsync(new Category("Category", "Image uri", null));
        
        var command = new AddProductCommand(
            Name: "Test Product",
            Description: "A test product",
            Image: "image uri",
            CategoryId: existingCategoryId,
            Price: 15,
            Amount: 1
            );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBe(Guid.Empty);
        _productRepositoryMock.Verify(x => x.AddAsync(It.Is<Product>(p =>
            p.Name == command.Name &&
            p.Description == command.Description &&
            p.Image == command.Image &&
            p.CategoryId == command.CategoryId &&
            p.Price.Value == command.Price &&
            p.Amount == command.Amount
            )), Times.Once);
    }
    
    [Fact]
    public async Task Handle_WithInValidCategory_ShouldAddProduct()
    {
        // Arrange
        _categoryRepositoryMock.Setup(repo => repo.GetAsync(existingCategoryId))
            .ReturnsAsync(new Category("Category", "Image uri", null));
        
        var command = new AddProductCommand(
            Name: "Test Product",
            Description: "A test product",
            Image: "image uri",
            CategoryId: Guid.NewGuid(),
            Price: 15,
            Amount: 1
            );

        // Act
        var result = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await result.Should().ThrowAsync<NotFoundException>();
        _productRepositoryMock.Verify(x => x.AddAsync(It.Is<Product>(p =>
            p.Name == command.Name &&
            p.Description == command.Description &&
            p.Image == command.Image &&
            p.CategoryId == command.CategoryId &&
            p.Price.Value == command.Price &&
            p.Amount == command.Amount
            )), Times.Never);
    }
}