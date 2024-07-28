using PosTech.MyFood.WebApi.Features.Orders.Commands;
using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Services;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Commands;

public class UpdateOrderQueueStatusCommandTests
{
    private readonly UpdateOrderQueueStatusCommand.Handler _handler;
    private readonly IOrderQueueService _orderQueueService;
    private readonly UpdateOrderQueueStatusCommand.Validator _validator;

    public UpdateOrderQueueStatusCommandTests()
    {
        _orderQueueService = Substitute.For<IOrderQueueService>();
        _handler = new UpdateOrderQueueStatusCommand.Handler(_orderQueueService);
        _validator = new UpdateOrderQueueStatusCommand.Validator();
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenIdIsEmpty()
    {
        var command = new UpdateOrderQueueStatusCommand.Command
        {
            Id = Guid.Empty,
            Status = OrderQueueStatus.Received
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenStatusIsInvalid()
    {
        var command = new UpdateOrderQueueStatusCommand.Command
        {
            Id = Guid.NewGuid(),
            Status = (OrderQueueStatus)999 // Invalid status
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status);
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenCommandIsValid()
    {
        var command = new UpdateOrderQueueStatusCommand.Command
        {
            Id = Guid.NewGuid(),
            Status = OrderQueueStatus.Received
        };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
        result.ShouldNotHaveValidationErrorFor(x => x.Status);
    }

    [Fact]
    public async Task Handler_ShouldReturnSuccess_WhenOrderStatusIsUpdatedSuccessfully()
    {
        // Arrange
        var command = new UpdateOrderQueueStatusCommand.Command
        {
            Id = Guid.NewGuid(),
            Status = OrderQueueStatus.Preparing
        };

        var response = new EnqueueOrderResponse
        {
            Id = command.Id,
            CreatedAt = DateTime.UtcNow,
            CustomerCpf = "12345678901",
            Status = OrderQueueStatus.Preparing,
            Items = new List<OrderItemDto>()
        };

        _orderQueueService
            .UpdateOrderStatusAsync(Arg.Any<Guid>(), Arg.Any<OrderQueueStatus>(), Arg.Any<CancellationToken>())
            .Returns(Result.Success(response));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task Handler_ShouldReturnFailure_WhenOrderIsNotFound()
    {
        // Arrange
        var command = new UpdateOrderQueueStatusCommand.Command
        {
            Id = Guid.NewGuid(),
            Status = OrderQueueStatus.Preparing
        };

        _orderQueueService
            .UpdateOrderStatusAsync(Arg.Any<Guid>(), Arg.Any<OrderQueueStatus>(), Arg.Any<CancellationToken>())
            .Returns(Result.Failure<EnqueueOrderResponse>(Error.Failure("OrderQueueService.UpdateOrderStatusAsync",
                $"Order with id {command.Id} not found.")));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain($"Order with id {command.Id} not found.");
    }
}