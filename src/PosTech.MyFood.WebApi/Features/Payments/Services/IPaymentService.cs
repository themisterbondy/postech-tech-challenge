using PosTech.MyFood.WebApi.Features.Payments.Contracts;
using PosTech.MyFood.WebApi.Features.Payments.Notifications;

namespace PosTech.MyFood.WebApi.Features.Payments.Services;

public interface IPaymentService
{
    Task<Result<PaymentInitiationResponse>> InitiatePaymentAsync(Guid cartId, decimal amount);
    Task<Result<PaymentStatusResponse>> GetPaymentStatusAsync(Guid cartId);
    Task<Result> ProcessPaymentNotificationAsync(PaymentNotification notification);
}