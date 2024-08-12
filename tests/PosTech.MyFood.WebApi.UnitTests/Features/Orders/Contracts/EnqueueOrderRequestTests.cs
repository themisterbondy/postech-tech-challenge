using PosTech.MyFood.WebApi.Features.Orders.Contracts;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Contracts;

public class EnqueueOrderRequestTests
{
    [Fact]
    public void EnqueueOrderRequest_ShouldInitializeCorrectly_WhenValidParametersAreProvided()
    {
        // Arrange
        var customerCpf = "12345678901";
        var items = new List<OrderItemRequest>
        {
            new() { ProductId = Guid.NewGuid(), Quantity = 2 }
        };

        // Act
        var request = new EnqueueOrderRequest
        {
            CustomerCpf = customerCpf,
            Items = items
        };

        // Assert
        request.Should().NotBeNull();
        request.CustomerCpf.Should().Be(customerCpf);
        request.Items.Should().BeEquivalentTo(items);
    }

    [Fact]
    public void EnqueueOrderRequest_ShouldInitializeCorrectly_WhenCustomerCpfIsNull()
    {
        // Arrange
        string customerCpf = null;
        var items = new List<OrderItemRequest>
        {
            new() { ProductId = Guid.NewGuid(), Quantity = 2 }
        };

        // Act
        var request = new EnqueueOrderRequest
        {
            CustomerCpf = customerCpf,
            Items = items
        };

        // Assert
        request.Should().NotBeNull();
        request.CustomerCpf.Should().BeNull();
        request.Items.Should().BeEquivalentTo(items);
    }

    [Fact]
    public void EnqueueOrderRequest_ShouldInitializeCorrectly_WhenItemsListIsEmpty()
    {
        // Arrange
        var customerCpf = "12345678901";
        var items = new List<OrderItemRequest>();

        // Act
        var request = new EnqueueOrderRequest
        {
            CustomerCpf = customerCpf,
            Items = items
        };

        // Assert
        request.Should().NotBeNull();
        request.CustomerCpf.Should().Be(customerCpf);
        request.Items.Should().BeEmpty();
    }
}

public class OrderItemRequestTests
{
    [Fact]
    public void OrderItemRequest_ShouldInitializeCorrectly_WhenValidParametersAreProvided()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var quantity = 2;

        // Act
        var request = new OrderItemRequest
        {
            ProductId = productId,
            Quantity = quantity
        };

        // Assert
        request.Should().NotBeNull();
        request.ProductId.Should().Be(productId);
        request.Quantity.Should().Be(quantity);
    }

    [Fact]
    public void OrderItemRequest_ShouldInitializeCorrectly_WhenQuantityIsZero()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var quantity = 0;

        // Act
        var request = new OrderItemRequest
        {
            ProductId = productId,
            Quantity = quantity
        };

        // Assert
        request.Should().NotBeNull();
        request.ProductId.Should().Be(productId);
        request.Quantity.Should().Be(quantity);
    }

    [Fact]
    public void OrderItemRequest_ShouldInitializeCorrectly_WhenQuantityIsNegative()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var quantity = -1;

        // Act
        var request = new OrderItemRequest
        {
            ProductId = productId,
            Quantity = quantity
        };

        // Assert
        request.Should().NotBeNull();
        request.ProductId.Should().Be(productId);
        request.Quantity.Should().Be(quantity);
    }
}