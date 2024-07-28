using PosTech.MyFood.WebApi.Features.Orders.Commands;
using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Services;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Commands;

public class CreateOrderCommandTests
{
    private readonly CreateOrderCommand.Handler _handler;
    private readonly IOrderQueueService _orderQueueService;
    private readonly CreateOrderCommand.Validator _validator;

    public CreateOrderCommandTests()
    {
        _orderQueueService = Substitute.For<IOrderQueueService>();
        _handler = new CreateOrderCommand.Handler(_orderQueueService);
        _validator = new CreateOrderCommand.Validator();
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenProductIdIsEmpty()
    {
        var command = new CreateOrderCommand.Command
        {
            Items = new List<OrderItemRequest>
            {
                new() { ProductId = Guid.Empty, Quantity = 1 }
            }
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor("Items[0].ProductId");
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenQuantityIsLessThanOrEqualToZero()
    {
        var command = new CreateOrderCommand.Command
        {
            Items = new List<OrderItemRequest>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 0 }
            }
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor("Items[0].Quantity");
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenItemsAreValid()
    {
        var command = new CreateOrderCommand.Command
        {
            Items = new List<OrderItemRequest>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 1 }
            }
        };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor("Items[0].ProductId");
        result.ShouldNotHaveValidationErrorFor("Items[0].Quantity");
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenCustomerCpfIsInvalid()
    {
        var command = new CreateOrderCommand.Command
        {
            CustomerCpf = "123456",
            Items = new List<OrderItemRequest>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 1 }
            }
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CustomerCpf);
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenCustomerCpfIsValid()
    {
        var command = new CreateOrderCommand.Command
        {
            CustomerCpf = "12345678901",
            Items = new List<OrderItemRequest>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 1 }
            }
        };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.CustomerCpf);
    }

    [Fact]
    public async Task Handler_ShouldReturnSuccess_WhenOrderIsEnqueuedSuccessfully()
    {
        // Arrange
        var command = new CreateOrderCommand.Command
        {
            CustomerCpf = "12345678901",
            Items = new List<OrderItemRequest>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 1 }
            }
        };

        var response = new EnqueueOrderResponse
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            CustomerCpf = "12345678901",
            Status = OrderQueueStatus.Received,
            Items = new List<OrderItemDto>()
        };

        _orderQueueService.EnqueueOrderAsync(Arg.Any<EnqueueOrderRequest>(), Arg.Any<CancellationToken>())
            .Returns(Result.Success(response));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task Handler_ShouldReturnFailure_WhenCustomerNotFound()
    {
        // Arrange
        var command = new CreateOrderCommand.Command
        {
            CustomerCpf = "12345678901",
            Items = new List<OrderItemRequest>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 1 }
            }
        };

        _orderQueueService.EnqueueOrderAsync(Arg.Any<EnqueueOrderRequest>(), Arg.Any<CancellationToken>())
            .Returns(Result.Failure<EnqueueOrderResponse>(Error.Failure("OrderQueueService.EnqueueOrderAsync",
                $"Customer with cpf {command.CustomerCpf} not found.")));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain($"Customer with cpf {command.CustomerCpf} not found.");
    }

    [Fact]
    public async Task Handler_ShouldReturnFailure_WhenProductNotFound()
    {
        // Arrange
        var command = new CreateOrderCommand.Command
        {
            Items = new List<OrderItemRequest>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 1 }
            }
        };

        _orderQueueService.EnqueueOrderAsync(Arg.Any<EnqueueOrderRequest>(), Arg.Any<CancellationToken>())
            .Returns(Result.Failure<EnqueueOrderResponse>(Error.Failure("OrderQueueService.EnqueueOrderAsync",
                $"Product with id {command.Items[0].ProductId} not found.")));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain($"Product with id {command.Items[0].ProductId} not found.");
    }
}