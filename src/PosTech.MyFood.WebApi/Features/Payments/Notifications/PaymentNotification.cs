using PosTech.MyFood.WebApi.Features.Payments.Emun;

namespace PosTech.MyFood.WebApi.Features.Payments.Notifications;

public class PaymentNotification
{
    public string TransactionId { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal Amount { get; set; }
}