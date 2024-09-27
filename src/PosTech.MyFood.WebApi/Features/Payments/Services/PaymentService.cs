using PosTech.MyFood.WebApi.Features.Carts.Entities;
using PosTech.MyFood.WebApi.Features.Carts.Repositories;
using PosTech.MyFood.WebApi.Features.Carts.Services;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Repositories;
using PosTech.MyFood.WebApi.Features.Payments.Contracts;
using PosTech.MyFood.WebApi.Features.Payments.Emun;
using PosTech.MyFood.WebApi.Features.Payments.Notifications;

namespace PosTech.MyFood.WebApi.Features.Payments.Services;

public class PaymentService(
    ICartRepository cartRepository,
    IOrderQueueRepository orderQueueRepository,
    ICartService cartService) : IPaymentService
{
    public async Task<Result<PaymentInitiationResponse>> InitiatePaymentAsync(Guid cartId, decimal amount)
    {
        return await Task.FromResult(new PaymentInitiationResponse
        {
            TransactionId = Guid.NewGuid().ToString(),
            QrCodeImageUrl = "https://example.com/qrcode.png"
        });
    }

    public async Task<Result<PaymentStatusResponse>> GetPaymentStatusAsync(Guid cartId)
    {
        var cart = await cartRepository.GetByIdAsync(new CartId(cartId));
        if (cart == null)
            return Result.Failure<PaymentStatusResponse>(Error.Failure("PaymentService.GetPaymentStatusAsync",
                "Cart not found"));

        return new PaymentStatusResponse
        {
            CartId = cart.Id.Value,
            Status = cart.PaymentStatus,
            TransactionId = cart.TransactionId
        };
    }

    public async Task<Result> ProcessPaymentNotificationAsync(PaymentNotification notification)
    {
        // 1. Buscar o carrinho pelo TransactionId
        var cart = await cartRepository.GetByTransactionIdAsync(notification.TransactionId);
        if (cart == null)
            return Result.Failure(Error.Failure("PaymentService.ProcessPaymentNotificationAsync",
                "Cart not found or already processed"));

        var order = orderQueueRepository.GetByTransactionIdAsync(notification.TransactionId);
        if (order != null)
            return Result.Failure(Error.Failure("PaymentService.ProcessPaymentNotificationAsync",
                "Order already exists for this transaction"));

        if (notification.Status == PaymentStatus.Accepted)
        {
            cart.UpdatePaymentStatus(PaymentStatus.Accepted);

            var orderId = OrderId.New();
            var orderItems = cart.Items.Select(cartItem =>
                OrderItem.Create(OrderItemId.New(), orderId, cartItem.ProductId, cartItem.ProductName,
                    cartItem.UnitPrice, cartItem.Quantity, cartItem.Category)).ToList();

            var orderQueue = OrderQueue.Create(orderId, IsAnonymousCustomer(cart.CustomerId), orderItems,
                notification.TransactionId, OrderQueueStatus.Received);

            await orderQueueRepository.AddAsync(orderQueue, CancellationToken.None);

            await cartService.ClearCartAsync(cart.Id.Value);
        }
        else if (notification.Status == PaymentStatus.Rejected)
        {
            cart.UpdatePaymentStatus(PaymentStatus.Rejected);
            await cartRepository.UpdateAsync(cart);
        }

        return Result.Success();
    }


    private static string? IsAnonymousCustomer(string customerId)
    {
        return Guid.TryParse(customerId, out _)
            ? null
            : customerId;
    }
}