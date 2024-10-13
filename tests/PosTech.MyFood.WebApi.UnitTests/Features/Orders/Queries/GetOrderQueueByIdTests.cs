using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Queries;
using PosTech.MyFood.WebApi.Features.Orders.Services;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Queries;

public class GetOrderQueueByIdTests
{
    private readonly GetOrderQueueById.Handler _handler;
    private readonly IOrderQueueService _orderQueueService;

    public GetOrderQueueByIdTests()
    {
        _orderQueueService = Substitute.For<IOrderQueueService>();
        _handler = new GetOrderQueueById.Handler(_orderQueueService);
    }

    [Fact]
    public async Task Handler_ShouldReturnSuccess_WhenOrderIsFound()
    {
        // Arrange
        var query = new GetOrderQueueById.Query
        {
            Id = Guid.NewGuid()
        };

        var response = new EnqueueOrderResponse
        {
            OrderId = query.Id,
            CreatedAt = DateTime.UtcNow,
            CustomerCpf = "12345678901",
            Status = OrderQueueStatus.Received,
            Items = new List<OrderItemDto>()
        };

        _orderQueueService.GetOrderByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(Result.Success(response));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task Handler_ShouldReturnFailure_WhenOrderIsNotFound()
    {
        // Arrange
        var query = new GetOrderQueueById.Query
        {
            Id = Guid.NewGuid()
        };

        _orderQueueService.GetOrderByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(Result.Failure<EnqueueOrderResponse>(Error.Failure("OrderQueueService.UpdateOrderStatusAsync",
                $"Order with id {query.Id} not found.")));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain($"Order with id {query.Id} not found.");
    }
}