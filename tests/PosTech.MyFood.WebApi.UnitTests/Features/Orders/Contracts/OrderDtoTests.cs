using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Contracts;

public class OrderDtoTests
{
    [Fact]
    public void OrderDto_ShouldInitializeCorrectly_WhenValidParametersAreProvided()
    {
        // Arrange
        var id = Guid.NewGuid();
        var orderDate = DateTime.UtcNow;
        var status = "Received";
        var customerCpf = "12345678901";
        var items = new List<OrderItemDto>
        {
            new()
            {
                ProductId = Guid.NewGuid(), ProductName = "Test Product", ProductDescription = "Test Description",
                UnitPrice = 10.99m, Quantity = 2, Category = ProductCategory.Lanche
            }
        };

        // Act
        var orderDto = new OrderDto
        {
            OrderId = id,
            OrderDate = orderDate,
            Status = status,
            CustomerCpf = customerCpf,
            Items = items
        };

        // Assert
        orderDto.Should().NotBeNull();
        orderDto.OrderId.Should().Be(id);
        orderDto.OrderDate.Should().Be(orderDate);
        orderDto.Status.Should().Be(status);
        orderDto.CustomerCpf.Should().Be(customerCpf);
        orderDto.Items.Should().BeEquivalentTo(items);
    }

    [Fact]
    public void OrderDto_ShouldInitializeCorrectly_WhenCustomerCpfIsNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var orderDate = DateTime.UtcNow;
        var status = "Received";
        string customerCpf = null;
        var items = new List<OrderItemDto>
        {
            new()
            {
                ProductId = Guid.NewGuid(), ProductName = "Test Product", ProductDescription = "Test Description",
                UnitPrice = 10.99m, Quantity = 2, Category = ProductCategory.Lanche
            }
        };

        // Act
        var orderDto = new OrderDto
        {
            OrderId = id,
            OrderDate = orderDate,
            Status = status,
            CustomerCpf = customerCpf,
            Items = items
        };

        // Assert
        orderDto.Should().NotBeNull();
        orderDto.OrderId.Should().Be(id);
        orderDto.OrderDate.Should().Be(orderDate);
        orderDto.Status.Should().Be(status);
        orderDto.CustomerCpf.Should().BeNull();
        orderDto.Items.Should().BeEquivalentTo(items);
    }

    [Fact]
    public void OrderDto_ShouldInitializeCorrectly_WhenItemsListIsEmpty()
    {
        // Arrange
        var id = Guid.NewGuid();
        var orderDate = DateTime.UtcNow;
        var status = "Received";
        var customerCpf = "12345678901";
        var items = new List<OrderItemDto>();

        // Act
        var orderDto = new OrderDto
        {
            OrderId = id,
            OrderDate = orderDate,
            Status = status,
            CustomerCpf = customerCpf,
            Items = items
        };

        // Assert
        orderDto.Should().NotBeNull();
        orderDto.OrderId.Should().Be(id);
        orderDto.OrderDate.Should().Be(orderDate);
        orderDto.Status.Should().Be(status);
        orderDto.CustomerCpf.Should().Be(customerCpf);
        orderDto.Items.Should().BeEmpty();
    }
}