using FluentValidation;
using PosTech.MyFood.WebApi.Common.Validation;
using PosTech.MyFood.WebApi.Features.Carts.Repositories;
using PosTech.MyFood.WebApi.Features.Carts.Services;
using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Repositories;
using PosTech.MyFood.WebApi.Features.Payments.Services;

namespace PosTech.MyFood.WebApi.Features.Orders.Commands;

public class FakeCheckout
{
    public class Command : IRequest<Result<CheckoutResponse>>
    {
        public string CustomerId { get; set; }
    }

    public class FakeCheckoutValidator : AbstractValidator<Command>
    {
        public FakeCheckoutValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithError(Error.Validation("CustomerId", "Identifier is required."))
                .NotNull().WithError(Error.Validation("CustomerId", "Identifier is required."));
        }
    }

    public class Handler(
        ICartRepository cartRepository,
        ICartService cartService,
        IOrderQueueRepository orderQueueRepository,
        IPaymentService paymentService)
        : IRequestHandler<Command, Result<CheckoutResponse>>
    {
        public async Task<Result<CheckoutResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetByCustomerIdAsync(request.CustomerId);
            if (cart == null || cart.Items.Count == 0)
                return Result.Failure<CheckoutResponse>(Error.Failure("FakeCheckout.Handler",
                    "Cart is empty or not found."));

            var totalAmount = cart.Items.Sum(item => item.UnitPrice * item.Quantity);

            // Process the payment
            var paymentResult = await paymentService.ProcessPaymentAsync(request.CustomerId, totalAmount);

            if (paymentResult.Status == PaymentStatus.Rejected)
                return Result.Failure<CheckoutResponse>(Error.Failure("FakeCheckout.Handler", "Payment was rejected."));

            var orderId = OrderId.New();
            var orderItems = cart.Items.Select(cartItem =>
                OrderItem.Create(OrderItemId.New(), orderId, cartItem.ProductId, cartItem.ProductName,
                    cartItem.UnitPrice, cartItem.Quantity, cartItem.Category)).ToList();

            var orderQueue = OrderQueue.Create(orderId, DateTime.UtcNow, IsAnonymousCustomer(request.CustomerId),
                orderItems);

            await orderQueueRepository.AddAsync(orderQueue, cancellationToken);

            await cartService.ClearCartAsync(request.CustomerId);

            return Result.Success(new CheckoutResponse
            {
                OrderId = orderQueue.Id.Value,
                CustomerId = request.CustomerId,
                Status = "Payment Accepted",
                TotalAmount = totalAmount,
                Items = orderQueue.Items.Select(item => new OrderItemDto
                {
                    ProductId = item.ProductId.Value,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                }).ToList()
            });
        }

        private string? IsAnonymousCustomer(string customerId)
        {
            return Guid.TryParse(customerId, out _)
                ? null
                : customerId;
        }
    }
}