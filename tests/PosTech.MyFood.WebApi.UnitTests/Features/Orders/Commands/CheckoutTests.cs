using PosTech.MyFood.WebApi.Features.Carts.Commands;
using PosTech.MyFood.WebApi.Features.Carts.Entities;
using PosTech.MyFood.WebApi.Features.Carts.Repositories;
using PosTech.MyFood.WebApi.Features.Carts.Services;
using PosTech.MyFood.WebApi.Features.Orders.Commands;
using PosTech.MyFood.WebApi.Features.Orders.Repositories;
using PosTech.MyFood.WebApi.Features.Payments.Services;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Commands;

public class CheckoutTests
{
    [Fact]
    public void Validator_ReturnsError_WhenCustomerIdIsEmpty()
    {
        var validator = new Checkout.FakeCheckoutValidator();
        var command = new Checkout.Command { CartId = Guid.Empty };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.CartId)
            .WithErrorCode("CartId")
            .WithErrorMessage("Cart Identifier is required.");
    }

    [Fact]
    public void Validator_Passes_WhenCustomerIdIsValid()
    {
        var validator = new Checkout.FakeCheckoutValidator();
        var command = new Checkout.Command { CartId = Guid.NewGuid() };

        var result = validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.CartId);
    }

    // [Fact]
    // public async Task Handle_ReturnsFailure_WhenCartIsEmpty()
    // {
    //     var cartRepository = Substitute.For<ICartRepository>();
    //     var cartService = Substitute.For<ICartService>();
    //     var orderQueueRepository = Substitute.For<IOrderQueueRepository>();
    //     var paymentService = Substitute.For<IPaymentService>();
    //     var handler = new Checkout.Handler(cartRepository, cartService, orderQueueRepository, paymentService);
    //     var command = new Checkout.Command { CartId = Guid.NewGuid() };
    //
    //     cartRepository.GetByIdAsync(new CartId(command.CartId)).Returns(Task.FromResult<Cart>(null));
    //
    //     var result = await handler.Handle(command, CancellationToken.None);
    //
    //     result.IsFailure.Should().BeTrue();
    //     result.Error.Code.Should().Be("FakeCheckout.Handler");
    //     result.Error.Message.Should().Be("Cart is empty or not found.");
    // }
}