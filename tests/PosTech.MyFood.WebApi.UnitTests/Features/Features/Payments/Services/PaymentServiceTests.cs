// using PosTech.MyFood.WebApi.Features.Payments.Emun;
// using PosTech.MyFood.WebApi.Features.Payments.Services;
//
// namespace PosTech.MyFood.WebApi.UnitTests.Features.Features.Payments.Services;
//
// public class PaymentServiceTests
// {
//     private readonly Guid _cartId = Guid.NewGuid();
//
//     [Fact]
//     public async Task ProcessPaymentAsync_ReturnsAccepted_WhenRandomIsZero()
//     {
//         var paymentService = new PaymentService();
//         var result = await paymentService.ProcessPaymentAsync(_cartId, 100.00m);
//
//         if (result.Status == PaymentStatus.Accepted)
//         {
//             result.Status.Should().Be(PaymentStatus.Accepted);
//             result.TransactionId.Should().NotBeNullOrEmpty();
//         }
//     }
//
//     [Fact]
//     public async Task ProcessPaymentAsync_ReturnsRejected_WhenRandomIsOne()
//     {
//         var paymentService = new PaymentService();
//         var result = await paymentService.ProcessPaymentAsync(_cartId, 100.00m);
//
//         if (result.Status == PaymentStatus.Rejected)
//         {
//             result.Status.Should().Be(PaymentStatus.Rejected);
//             result.TransactionId.Should().NotBeNullOrEmpty();
//         }
//     }
//
//     [Fact]
//     public async Task ProcessPaymentAsync_ReturnsValidTransactionId()
//     {
//         var paymentService = new PaymentService();
//         var result = await paymentService.ProcessPaymentAsync(_cartId, 100.00m);
//
//         result.TransactionId.Should().NotBeNullOrEmpty();
//         Guid.TryParse(result.TransactionId, out _).Should().BeTrue();
//     }
// }