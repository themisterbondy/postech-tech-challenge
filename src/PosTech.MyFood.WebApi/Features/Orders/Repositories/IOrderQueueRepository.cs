using PosTech.MyFood.WebApi.Features.Orders.Entities;

namespace PosTech.MyFood.WebApi.Features.Orders.Repositories;

public interface IOrderQueueRepository
{
    Task<OrderQueue?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(OrderQueue orderQueue, CancellationToken cancellationToken);
    Task UpdateStatusAsync(Guid id, OrderQueueStatus status, CancellationToken cancellationToken);
}