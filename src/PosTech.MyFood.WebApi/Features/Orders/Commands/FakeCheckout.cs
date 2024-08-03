using FluentValidation;
using PosTech.MyFood.Features.Carts.Repositories;
using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Repositories;
using PosTech.MyFood.WebApi.Features.Payments.Services;

namespace PosTech.MyFood.WebApi.Features.Orders.Commands;

public class FakeCheckout
{
    public class Command : IRequest<Result<CheckoutResponse>>
    {
        public string CustomerCpf { get; set; }
    }

    public class FakeCheckoutValidator : AbstractValidator<Command>
    {
        public FakeCheckoutValidator()
        {
            RuleFor(x => x.CustomerCpf).NotEmpty().WithMessage("CustomerCpf is required.");
        }
    }

    public class FakeCheckoutHandler(
        ICartRepository cartRepository,
        IOrderQueueRepository orderQueueRepository,
        IPaymentService paymentService)
        : IRequestHandler<Command, Result<CheckoutResponse>>
    {
        public async Task<Result<CheckoutResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetByCustomerCpfAsync(request.CustomerCpf);
            if (cart == null || cart.Items.Count == 0)
                return Result.Failure<CheckoutResponse>(Error.Failure("FakeCheckoutHandler.Handle",
                    "Cart is empty or not found."));

            var totalAmount = cart.Items.Sum(item => item.UnitPrice * item.Quantity);

            // Process the payment
            var paymentResult = await paymentService.ProcessPaymentAsync(request.CustomerCpf, totalAmount);

            if (paymentResult.Status == PaymentStatus.Rejected)
                return new CheckoutResponse
                {
                    OrderId = Guid.Empty,
                    Status = "Payment Rejected",
                    Items = new List<OrderItemDto>()
                };

            var orderId = OrderId.New();
            var orderItems = cart.Items.Select(cartItem =>
                OrderItem.Create(OrderItemId.New(), orderId, cartItem.ProductId, cartItem.ProductName,
                    cartItem.UnitPrice, cartItem.Quantity, cartItem.Category)).ToList();

            var orderQueue = OrderQueue.Create(orderId, DateTime.UtcNow, request.CustomerCpf, orderItems);

            await orderQueueRepository.AddAsync(orderQueue, cancellationToken);

            return new CheckoutResponse
            {
                OrderId = orderQueue.Id.Value,
                Status = "Payment Accepted",
                Items = orderQueue.Items.Select(item => new OrderItemDto
                {
                    ProductId = item.ProductId.Value,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                }).ToList()
            };
        }
    }
}