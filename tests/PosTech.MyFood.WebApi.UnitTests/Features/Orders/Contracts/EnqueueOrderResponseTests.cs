using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Contracts;

public class EnqueueOrderResponseTests
{
    [Fact]
    public void EnqueueOrderResponse_ShouldInitializeCorrectly_WhenValidParametersAreProvided()
    {
        // Arrange
        var id = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;
        var customerCpf = "12345678901";
        var status = OrderQueueStatus.Received;
        var items = new List<OrderItemDto>
        {
            new()
            {
                ProductId = Guid.NewGuid(), ProductName = "Test Product", ProductDescription = "Test Description",
                UnitPrice = 10.99m, Quantity = 2, Category = ProductCategory.Lanche
            }
        };

        // Act
        var response = new EnqueueOrderResponse
        {
            Id = id,
            CreatedAt = createdAt,
            CustomerCpf = customerCpf,
            Status = status,
            Items = items
        };

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(id);
        response.CreatedAt.Should().Be(createdAt);
        response.CustomerCpf.Should().Be(customerCpf);
        response.Status.Should().Be(status);
        response.Items.Should().BeEquivalentTo(items);
    }
}

public class ListOrdersResponseTests
{
    [Fact]
    public void ListOrdersResponse_ShouldInitializeCorrectly_WhenValidParametersAreProvided()
    {
        // Arrange
        var orders = new List<OrderDto>
        {
            new()
            {
                Id = Guid.NewGuid(), OrderDate = DateTime.UtcNow, CustomerCpf = "12345678901", Items =
                [
                    new OrderItemDto
                    {
                        ProductId = Guid.NewGuid(), ProductName = "Test Product",
                        ProductDescription = "Test Description", UnitPrice = 10.99m, Quantity = 2,
                        Category = ProductCategory.Lanche
                    }
                ]
            }
        };

        // Act
        var response = new ListOrdersResponse
        {
            Orders = orders
        };

        // Assert
        response.Should().NotBeNull();
        response.Orders.Should().BeEquivalentTo(orders);
    }

    [Fact]
    public void ListOrdersResponse_ShouldInitializeCorrectly_WhenOrdersListIsEmpty()
    {
        // Arrange
        var orders = new List<OrderDto>();

        // Act
        var response = new ListOrdersResponse
        {
            Orders = orders
        };

        // Assert
        response.Should().NotBeNull();
        response.Orders.Should().BeEmpty();
    }
}

public class OrderItemDtoTests
{
    [Fact]
    public void OrderItemDto_ShouldInitializeCorrectly_WhenValidParametersAreProvided()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var productName = "Test Product";
        var productDescription = "Test Description";
        var unitPrice = 10.99m;
        var quantity = 2;
        var category = ProductCategory.Lanche;

        // Act
        var orderItemDto = new OrderItemDto
        {
            ProductId = productId,
            ProductName = productName,
            ProductDescription = productDescription,
            UnitPrice = unitPrice,
            Quantity = quantity,
            Category = category
        };

        // Assert
        orderItemDto.Should().NotBeNull();
        orderItemDto.ProductId.Should().Be(productId);
        orderItemDto.ProductName.Should().Be(productName);
        orderItemDto.ProductDescription.Should().Be(productDescription);
        orderItemDto.UnitPrice.Should().Be(unitPrice);
        orderItemDto.Quantity.Should().Be(quantity);
        orderItemDto.Category.Should().Be(category);
    }
}