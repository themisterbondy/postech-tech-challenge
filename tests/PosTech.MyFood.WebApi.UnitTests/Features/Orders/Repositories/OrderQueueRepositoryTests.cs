using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Repositories;
using PosTech.MyFood.WebApi.Persistence;
using PosTech.MyFood.WebApi.UnitTests.Mocks;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Repositories;

public class OrderQueueRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly OrderQueueRepository _repository;

    public OrderQueueRepositoryTests()
    {
        _context = ApplicationDbContextMock.Create();
        _repository = new OrderQueueRepository(_context);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnOrderQueue_WhenOrderQueueExists()
    {
        // Arrange
        var orderQueue = OrderQueueMocks.GenerateValidOrderQueue();
        _context.OrderQueue.Add(orderQueue);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(orderQueue.Id.Value, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(orderQueue.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenOrderQueueDoesNotExist()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await _repository.GetByIdAsync(nonExistentId, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_ShouldAddOrderQueueToDatabase()
    {
        // Arrange
        var orderQueue = OrderQueueMocks.GenerateValidOrderQueue();

        // Act
        await _repository.AddAsync(orderQueue, CancellationToken.None);
        var result = await _context.OrderQueue.FindAsync(orderQueue.Id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(orderQueue.Id);
    }

    [Fact]
    public async Task UpdateStatusAsync_ShouldUpdateOrderQueueStatus()
    {
        // Arrange
        var orderQueue = OrderQueueMocks.GenerateValidOrderQueue();
        _context.OrderQueue.Add(orderQueue);
        await _context.SaveChangesAsync();

        var newStatus = OrderQueueStatus.Preparing;

        // Act
        await _repository.UpdateStatusAsync(orderQueue.Id.Value, newStatus, CancellationToken.None);
        var result = await _context.OrderQueue.FindAsync(orderQueue.Id);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(newStatus);
    }
}