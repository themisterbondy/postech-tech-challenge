using Microsoft.EntityFrameworkCore;
using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Queries;
using PosTech.MyFood.WebApi.Features.Products.Entities;
using PosTech.MyFood.WebApi.Persistence;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Queries;

public class ListOrdersTests
{
    private readonly ApplicationDbContext _context;
    private readonly ListOrders.Handler _handler;

    public ListOrdersTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _handler = new ListOrders.Handler(_context);
    }

    [Fact]
    public async Task Handler_ShouldReturnOrders_WhenOrdersExist()
    {
        // Arrange
        var order = OrderQueue.Create(
            new OrderId(Guid.NewGuid()),
            DateTime.UtcNow,
            "12345678901",
            [
                OrderItem.Create(
                    new OrderItemId(Guid.NewGuid()),
                    new OrderId(Guid.NewGuid()),
                    new ProductId(Guid.NewGuid()),
                    "Test Product",
                    10.99m,
                    1,
                    ProductCategory.Lanche)
            ]);
        _context.OrderQueue.Add(order);
        await _context.SaveChangesAsync();

        var query = new ListOrders.Query();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Orders.Should().HaveCount(1);
        result.Value.Orders.First().Id.Should().Be(order.Id.Value);
    }

    [Fact]
    public async Task Handler_ShouldReturnEmptyList_WhenNoOrdersExist()
    {
        // Arrange
        var query = new ListOrders.Query();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Orders.Should().BeEmpty();
    }
}