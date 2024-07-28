using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Entities;

public class OrderItemTests
{
    [Fact]
    public void OrderItemId_ShouldInitializeCorrectly_WhenValidGuidIsProvided()
    {
        var guid = Guid.NewGuid();
        var orderItemId = new OrderItemId(guid);

        orderItemId.Should().NotBeNull();
        orderItemId.Value.Should().Be(guid);
    }

    [Fact]
    public void OrderItemId_ShouldGenerateNewGuid_WhenNewIsCalled()
    {
        var orderItemId = OrderItemId.New();

        orderItemId.Should().NotBeNull();
        orderItemId.Value.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void OrderItem_ShouldInitializeCorrectly_WhenValidParametersAreProvided()
    {
        var id = new OrderItemId(Guid.NewGuid());
        var orderId = new OrderId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var productName = "Test Product";
        var productDescription = "Test Description";
        var unitPrice = 10.99m;
        var quantity = 2;
        var category = ProductCategory.Lanche;

        var orderItem = OrderItem.Create(id, orderId, productId, productName, productDescription, unitPrice, quantity,
            category);

        orderItem.Should().NotBeNull();
        orderItem.Id.Should().Be(id);
        orderItem.OrderId.Should().Be(orderId);
        orderItem.ProductId.Should().Be(productId);
        orderItem.ProductName.Should().Be(productName);
        orderItem.ProductDescription.Should().Be(productDescription);
        orderItem.UnitPrice.Should().Be(unitPrice);
        orderItem.Quantity.Should().Be(quantity);
        orderItem.Category.Should().Be(category);
    }

    [Fact]
    public void OrderItem_ShouldInitializeCorrectly_WhenUnitPriceIsNull()
    {
        var id = new OrderItemId(Guid.NewGuid());
        var orderId = new OrderId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var productName = "Test Product";
        var productDescription = "Test Description";
        decimal? unitPrice = null;
        var quantity = 2;
        var category = ProductCategory.Lanche;

        var orderItem = OrderItem.Create(id, orderId, productId, productName, productDescription, unitPrice, quantity,
            category);

        orderItem.Should().NotBeNull();
        orderItem.Id.Should().Be(id);
        orderItem.OrderId.Should().Be(orderId);
        orderItem.ProductId.Should().Be(productId);
        orderItem.ProductName.Should().Be(productName);
        orderItem.ProductDescription.Should().Be(productDescription);
        orderItem.UnitPrice.Should().BeNull();
        orderItem.Quantity.Should().Be(quantity);
        orderItem.Category.Should().Be(category);
    }

    [Fact]
    public void OrderItem_ShouldInitializeCorrectly_WhenProductDescriptionIsNull()
    {
        var id = new OrderItemId(Guid.NewGuid());
        var orderId = new OrderId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var productName = "Test Product";
        string productDescription = null;
        var unitPrice = 10.99m;
        var quantity = 2;
        var category = ProductCategory.Lanche;

        var orderItem = OrderItem.Create(id, orderId, productId, productName, productDescription, unitPrice, quantity,
            category);

        orderItem.Should().NotBeNull();
        orderItem.Id.Should().Be(id);
        orderItem.OrderId.Should().Be(orderId);
        orderItem.ProductId.Should().Be(productId);
        orderItem.ProductName.Should().Be(productName);
        orderItem.ProductDescription.Should().BeNull();
        orderItem.UnitPrice.Should().Be(unitPrice);
        orderItem.Quantity.Should().Be(quantity);
        orderItem.Category.Should().Be(category);
    }
}