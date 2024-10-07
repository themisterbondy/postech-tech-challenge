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


        [Fact]
        public async Task ClearCartAsync_ShouldReturnNull_WhenCartNotFound()
        {
            // Arrange
            var cartService = new CartService(_cartRepository);
            var cartId = Guid.NewGuid();

            _cartRepository.GetByIdAsync(Arg.Any<CartId>()).Returns((Cart)null);

            // Act
            var result = await cartService.ClearCartAsync(cartId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task ClearCartAsync_ShouldClearItemsAndDeleteCart_WhenCartFound()
        {
            // Arrange
            var cartService = new CartService(_cartRepository);
            var cartId = new CartId(Guid.NewGuid());
            var cart = Cart.Create(cartId, "customer123");
            var cartItem = CartItem.Create(CartItemId.New(), ProductId.New(), "Product 1", 10.99m, 1, ProductCategory.Lanche);
            cart.Items.Add(cartItem);

            _cartRepository.GetByIdAsync(Arg.Is<CartId>(x => x == cartId)).Returns(cart);

            // Act
            var result = await cartService.ClearCartAsync(cartId.Value);

            // Assert
            result.Should().NotBeNull();
            result.CartId.Should().Be(cartId.Value);
            result.Items.Should().BeEmpty(); // Todos os itens devem ter sido removidos

            // Verifica se os itens foram removidos
            await _cartRepository.Received(1).Delete(cart);
        }
        [Fact]
        public async Task ClearCartAsync_ShouldReturnCartResponse_WhenCartIsDeleted()
        {
            // Arrange
            var cartService = new CartService(_cartRepository);
            var cartId = new CartId(Guid.NewGuid());
            var cart = Cart.Create(cartId, "customer123");

            // Configurando o mock para retornar o carrinho quando solicitado
            _cartRepository.GetByIdAsync(Arg.Is<CartId>(id => id == cartId)).Returns(cart);

            // Act
            var result = await cartService.ClearCartAsync(cartId.Value);

            // Assert
            result.Should().NotBeNull();
            result.CartId.Should().Be(cartId.Value);
            result.CustomerId.Should().Be(cart.CustomerId);
        }

}