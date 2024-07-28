using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.Features.Products.Repositories;
using PosTech.MyFood.WebApi.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Products.Queries;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Products.Queries;

public class ListProductsTests
{
    private readonly ListProducts.ListProductsHandler _handler;
    private readonly IProductRepository _productRepository;

    public ListProductsTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new ListProducts.ListProductsHandler(_productRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnProducts_WhenCategoryIsProvided()
    {
        // Arrange
        var category = ProductCategory.Lanche;
        var products = new List<Product>
        {
            Product.Create(ProductId.New(), "Product 1", "Description 1", 10, ProductCategory.Lanche,
                "http://example.com/image1.jpg"),
            Product.Create(ProductId.New(), "Product 2", "Description 2", 20, ProductCategory.Lanche,
                "http://example.com/image2.jpg")
        };

        _productRepository.FindByCategoryAsync(category, Arg.Any<CancellationToken>()).Returns(products);

        var query = new ListProducts.Query { Category = category };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Products.Should().HaveCount(2);
        result.Value.Products.Should().BeEquivalentTo(products, options => options.ExcludingMissingMembers());
    }

    [Fact]
    public async Task Handle_ShouldReturnAllProducts_WhenCategoryIsNotProvided()
    {
        // Arrange
        var products = new List<Product>
        {
            Product.Create(ProductId.New(), "Product 1", "Description 1", 10, ProductCategory.Lanche,
                "http://example.com/image1.jpg"),
            Product.Create(ProductId.New(), "Product 2", "Description 2", 20, ProductCategory.Lanche,
                "http://example.com/image2.jpg")
        };

        _productRepository.FindByCategoryAsync(null, Arg.Any<CancellationToken>()).Returns(products);

        var query = new ListProducts.Query { Category = null };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Products.Should().HaveCount(2);
        result.Value.Products.Should().BeEquivalentTo(products, options => options.ExcludingMissingMembers());
    }
}