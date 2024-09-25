using PosTech.MyFood.WebApi.Features.Payments.Contracts;
using PosTech.MyFood.WebApi.Features.Payments.Services;

namespace PosTech.MyFood.WebApi.Features.Payments.Querys;

public class GetPaymentStatus
{
    public class Query : IRequest<Result<PaymentStatusResponse>>
    {
        public Guid CartId { get; set; }
    }

    public class Handler(IPaymentService paymentService) : IRequestHandler<Query, Result<PaymentStatusResponse>>
    {
        public async Task<Result<PaymentStatusResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var paymentStatus = await paymentService.GetPaymentStatusAsync(request.CartId);
            return paymentStatus.IsFailure
                ? Result.Failure<PaymentStatusResponse>(paymentStatus.Error)
                : Result.Success(paymentStatus.Value);
        }
    }
}