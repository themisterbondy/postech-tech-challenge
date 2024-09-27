using PosTech.MyFood.WebApi.Features.Payments.Emun;

namespace PosTech.MyFood.WebApi.Features.Payments.Contracts;

public class PaymentStatusResponse
{
    public Guid CartId { get; set; }
    public PaymentStatus Status { get; set; }
    public string TransactionId { get; set; }
}