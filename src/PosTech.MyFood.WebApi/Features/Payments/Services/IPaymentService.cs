namespace PosTech.MyFood.WebApi.Features.Payments.Services;

public interface IPaymentService
{
    Task<PaymentResult> ProcessPaymentAsync(string customerCpf, decimal amount);
}

public enum PaymentStatus
{
    Accepted,
    Rejected
}

public class PaymentResult
{
    public PaymentStatus Status { get; set; }
    public string TransactionId { get; set; }
}