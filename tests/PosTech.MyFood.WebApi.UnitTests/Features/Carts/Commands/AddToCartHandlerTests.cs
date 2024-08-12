using PosTech.MyFood.WebApi.Features.Carts.Commands;
using PosTech.MyFood.WebApi.Features.Carts.Contracts;
using PosTech.MyFood.WebApi.Features.Carts.Services;
using PosTech.MyFood.WebApi.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Products.Repositories;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Carts.Commands;

public class AddToCartHandlerTests
{
    private readonly ICartService _cartService;
    private readonly AddToCart.Handler _handler;
    private readonly IProductRepository _productRepository;

    public AddToCartHandlerTests()
    {
        _cartService = Substitute.For<ICartService>();
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new AddToCart.Handler(_cartService, _productRepository);
    }

    [Fact]
    public async Task ShouldReturnFailureWhenProductNotFound()
    {
        _productRepository.FindByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns((Product)null);

        var command = new AddToCart.Command { ProductId = Guid.NewGuid(), Quantity = 1 };
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("AddToCart.Handler");
    }

    [Fact]
    public async Task ShouldAddToCartSuccessfullyWhenProductIsFound()
    {
        var product = Product.Create(new ProductId(Guid.NewGuid()), "Test Product", "Description", 10.99m,
            ProductCategory.Lanche, "http://example.com/image.jpg");
        _productRepository.FindByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(product);

        var command = new AddToCart.Command { CustomerId = "12345678901", ProductId = product.Id.Value, Quantity = 1 };
        _cartService
            .AddToCartAsync(Arg.Any<string>(), Arg.Any<CartItemDto>())
            .Returns(Task.FromResult(new CartResponse()));


        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
    }
}