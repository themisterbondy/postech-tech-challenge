using PosTech.MyFood.WebApi.Features.Carts.Entities;
using PosTech.MyFood.WebApi.Features.Carts.Repositories;
using PosTech.MyFood.WebApi.Features.Carts.Services;
using PosTech.MyFood.WebApi.Features.Orders.Commands;
using PosTech.MyFood.WebApi.Features.Orders.Repositories;
using PosTech.MyFood.WebApi.Features.Payments.Services;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Commands;

public class FakeCheckoutTests
{
    [Fact]
    public void Validator_ReturnsError_WhenCustomerIdIsEmpty()
    {
        var validator = new FakeCheckout.FakeCheckoutValidator();
        var command = new FakeCheckout.Command { CustomerId = string.Empty };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.CustomerId)
            .WithErrorCode("CustomerId")
            .WithErrorMessage("Identifier is required.");
    }

    [Fact]
    public void Validator_ReturnsError_WhenCustomerIdIsNull()
    {
        var validator = new FakeCheckout.FakeCheckoutValidator();
        var command = new FakeCheckout.Command { CustomerId = null };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.CustomerId)
            .WithErrorCode("CustomerId")
            .WithErrorMessage("Identifier is required.");
    }

    [Fact]
    public void Validator_Passes_WhenCustomerIdIsValid()
    {
        var validator = new FakeCheckout.FakeCheckoutValidator();
        var command = new FakeCheckout.Command { CustomerId = "12345678901" };

        var result = validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.CustomerId);
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenCartIsEmpty()
    {
        var cartRepository = Substitute.For<ICartRepository>();
        var cartService = Substitute.For<ICartService>();
        var orderQueueRepository = Substitute.For<IOrderQueueRepository>();
        var paymentService = Substitute.For<IPaymentService>();
        var handler = new FakeCheckout.Handler(cartRepository, cartService, orderQueueRepository, paymentService);
        var command = new FakeCheckout.Command { CustomerId = "12345678901" };

        cartRepository.GetByCustomerIdAsync(command.CustomerId).Returns(Task.FromResult<Cart>(null));

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("FakeCheckout.Handler");
        result.Error.Message.Should().Be("Cart is empty or not found.");
    }
}