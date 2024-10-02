using PosTech.MyFood.WebApi.Features.Carts.Contracts;
using PosTech.MyFood.WebApi.Features.Carts.Entities;
using PosTech.MyFood.WebApi.Features.Carts.Repositories;
using PosTech.MyFood.WebApi.Features.Carts.Services;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Carts.Services;

public class CartServiceTests
{
    private readonly ICartRepository _cartRepository;
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _cartService = new CartService(_cartRepository);
    }

    [Fact]
    public async Task ShouldAddNewItemToCart()
    {
        var customerId = "12345678901";
        var cartItem = new CartItemDto
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            UnitPrice = 10.99m,
            Quantity = 1
        };

        var product = Product.Create(new ProductId(cartItem.ProductId), cartItem.ProductName, "Description", cartItem.UnitPrice,
            ProductCategory.Lanche, "http://example.com/image.jpg");

        _cartRepository.GetByCustomerIdAsync(customerId).Returns((Cart)null);

        var result = await _cartService.AddToCartAsync(customerId, cartItem,product);

        result.CustomerId.Should().Be(customerId);
        result.Items.Should().ContainSingle(i => i.ProductId == cartItem.ProductId);
    }

    [Fact]
    public async Task ShouldUpdateExistingItemInCart()
    {
        var customerId = "12345678901";
        var productId = Guid.NewGuid();
        var cartItem = new CartItemDto
        {
            ProductId = productId,
            ProductName = "Test Product",
            UnitPrice = 10.99m,
            Quantity = 1
        };

        var product = Product.Create(new ProductId(cartItem.ProductId), cartItem.ProductName, "Description", cartItem.UnitPrice,
            ProductCategory.Lanche, "http://example.com/image.jpg");

        var existingCart = Cart.Create(CartId.New(), customerId);
        existingCart.AddItem(CartItem.Create(CartItemId.New(), new ProductId(productId), "Test Product", 10.99m, 1,
            ProductCategory.Lanche));
        _cartRepository.GetByCustomerIdAsync(customerId).Returns(existingCart);

        var result = await _cartService.AddToCartAsync(customerId, cartItem,product);

        result.Items.Should().ContainSingle(i => i.ProductId == productId && i.Quantity == 2);
    }

    [Fact]
    public async Task ShouldReturnNullWhenCartNotFoundForGetCartByCustomerId()
    {
        var customerId = "12345678901";
        _cartRepository.GetByCustomerIdAsync(customerId).Returns((Cart)null);

        var result = await _cartService.GetCartByCustomerIdAsync(customerId);

        result.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnCartWhenFoundForGetCartByCustomerId()
    {
        var customerId = "12345678901";
        var existingCart = Cart.Create(CartId.New(), customerId);
        _cartRepository.GetByCustomerIdAsync(customerId).Returns(existingCart);

        var result = await _cartService.GetCartByCustomerIdAsync(customerId);

        result.CustomerId.Should().Be(customerId);
    }

    [Fact]
    public async Task ShouldRemoveItemFromCart()
    {
        var customerId = "12345678901";
        var productId = Guid.NewGuid();
        var existingCart = Cart.Create(CartId.New(), customerId);
        var cartItem = CartItem.Create(CartItemId.New(), new ProductId(productId), "Test Product", 10.99m, 1,
            ProductCategory.Lanche);
        existingCart.AddItem(cartItem);
        _cartRepository.GetByCustomerIdAsync(customerId).Returns(existingCart);

        var result = await _cartService.RemoveFromCartAsync(customerId, productId);

        result.Items.Should().NotContain(i => i.ProductId == productId);
    }

    // [Fact]
    // public async Task ShouldClearCart()
    // {
    //     var customerId = "12345678901";
    //     var existingCart = Cart.Create(CartId.New(), customerId);
    //     existingCart.AddItem(CartItem.Create(CartItemId.New(), new ProductId(Guid.NewGuid()), "Test Product", 10.99m, 1,
    //         ProductCategory.Lanche));
    //     _cartRepository.GetByCustomerIdAsync(customerId).Returns(existingCart);
    //
    //     var result = await _cartService.ClearCartAsync(customerId);
    //
    //     result.Items.Should().BeEmpty();
    // }
}