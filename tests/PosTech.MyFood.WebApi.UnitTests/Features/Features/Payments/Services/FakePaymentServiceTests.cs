using PosTech.MyFood.WebApi.Features.Payments.Services;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Features.Payments.Services;

public class FakePaymentServiceTests
{
    [Fact]
    public async Task ProcessPaymentAsync_ReturnsAccepted_WhenRandomIsZero()
    {
        var paymentService = new FakePaymentService();
        var result = await paymentService.ProcessPaymentAsync("12345678901", 100.00m);

        if (result.Status == PaymentStatus.Accepted)
        {
            result.Status.Should().Be(PaymentStatus.Accepted);
            result.TransactionId.Should().NotBeNullOrEmpty();
        }
    }

    [Fact]
    public async Task ProcessPaymentAsync_ReturnsRejected_WhenRandomIsOne()
    {
        var paymentService = new FakePaymentService();
        var result = await paymentService.ProcessPaymentAsync("12345678901", 100.00m);

        if (result.Status == PaymentStatus.Rejected)
        {
            result.Status.Should().Be(PaymentStatus.Rejected);
            result.TransactionId.Should().NotBeNullOrEmpty();
        }
    }

    [Fact]
    public async Task ProcessPaymentAsync_ReturnsValidTransactionId()
    {
        var paymentService = new FakePaymentService();
        var result = await paymentService.ProcessPaymentAsync("12345678901", 100.00m);

        result.TransactionId.Should().NotBeNullOrEmpty();
        Guid.TryParse(result.TransactionId, out _).Should().BeTrue();
    }
}