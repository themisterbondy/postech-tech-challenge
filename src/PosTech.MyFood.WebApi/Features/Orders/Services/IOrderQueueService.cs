using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Entities;

namespace PosTech.MyFood.WebApi.Features.Orders.Services;

public interface IOrderQueueService
{
    Task<Result<EnqueueOrderResponse>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Result<EnqueueOrderResponse>> UpdateOrderStatusAsync(Guid id, OrderQueueStatus status,
        CancellationToken cancellationToken);

    Task<Result<EnqueueOrderResponse>> GetOrderByTransactionIdAsync(string TransactionId, CancellationToken cancellationToken);
}