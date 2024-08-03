namespace PosTech.MyFood.WebApi.Features.Payments.Services;

public class FakePaymentService : IPaymentService
{
    public Task<PaymentResult> ProcessPaymentAsync(string customerCpf, decimal amount)
    {
        // Simulate payment processing with a random result
        var random = new Random();
        var accepted = random.Next(2) == 0;

        return Task.FromResult(new PaymentResult
        {
            Status = accepted ? PaymentStatus.Accepted : PaymentStatus.Rejected,
            TransactionId = Guid.NewGuid().ToString()
        });
    }
}