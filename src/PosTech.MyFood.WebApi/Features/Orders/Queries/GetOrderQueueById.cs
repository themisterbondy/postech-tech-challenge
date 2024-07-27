using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Services;

namespace PosTech.MyFood.WebApi.Features.Orders.Queries;

public class GetOrderQueueById
{
    public class Query : IRequest<Result<EnqueueOrderResponse>>
    {
        public Guid Id { get; set; }
    }

    public class Handler(IOrderQueueService orderQueueService) : IRequestHandler<Query, Result<EnqueueOrderResponse>>
    {
        public async Task<Result<EnqueueOrderResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await orderQueueService.GetOrderByIdAsync(request.Id, cancellationToken);
        }
    }
}