using FluentValidation;
using PosTech.MyFood.WebApi.Common.Validation;
using PosTech.MyFood.WebApi.Features.Carts.Entities;
using PosTech.MyFood.WebApi.Features.Carts.Repositories;
using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Payments.Emun;
using PosTech.MyFood.WebApi.Features.Payments.Services;

namespace PosTech.MyFood.WebApi.Features.Carts.Commands;

public class Checkout
{
    public class Command : IRequest<Result<CheckoutResponse>>
    {
        public Guid CartId { get; init; }
    }

    public class FakeCheckoutValidator : AbstractValidator<Command>
    {
        public FakeCheckoutValidator()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithError(Error.Validation("CartId", "Cart Identifier is required."))
                .NotNull().WithError(Error.Validation("CartId", "Cart Identifier is required."));
        }
    }

    public class Handler(
        ICartRepository cartRepository,
        IPaymentService paymentService)
        : IRequestHandler<Command, Result<CheckoutResponse>>
    {
        public async Task<Result<CheckoutResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetByIdAsync(new CartId(request.CartId));
            if (cart == null || cart.Items.Count == 0)
                return Result.Failure<CheckoutResponse>(Error.Failure("FakeCheckout.Handler",
                    "Cart is empty or not found."));

            if (cart.PaymentStatus != PaymentStatus.NotStarted)
                return Result.Failure<CheckoutResponse>(Error.Failure("FakeCheckout.Handler",
                    "Cart has already been checked out."));

            var totalAmount = cart.Items.Sum(item => item.UnitPrice * item.Quantity);

            var paymentInitiationResult = await paymentService.InitiatePaymentAsync(cart.Id.Value, totalAmount);
            if (paymentInitiationResult == null)
                return Result.Failure<CheckoutResponse>(Error.Failure("FakeCheckout.Handler",
                    "Failed to initiate payment."));

            cart.TransactionId = paymentInitiationResult.Value.TransactionId;
            cart.UpdatePaymentStatus(PaymentStatus.Pending);
            await cartRepository.UpdateStatusAsync(cart);

            return Result.Success(new CheckoutResponse
            {
                CartId = cart.Id.Value,
                CustomerId = cart.CustomerId,
                Status = cart.PaymentStatus.ToString(),
                TotalAmount = totalAmount,
                QrCodeImageUrl = paymentInitiationResult.Value.QrCodeImageUrl,
                TransactionId = paymentInitiationResult.Value.TransactionId,
                Items = cart.Items.Select(item => new OrderItemDto
                {
                    ProductId = item.ProductId.Value,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Category = item.Category
                }).ToList()
            });
        }
    }
}