using Microsoft.EntityFrameworkCore;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Persistence;

namespace PosTech.MyFood.WebApi.Features.Orders.Repositories;

public class OrderQueueRepository(ApplicationDbContext context) : IOrderQueueRepository
{
    public async Task<OrderQueue?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.OrderQueue
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == new OrderId(id), cancellationToken);
    }

    public async Task AddAsync(OrderQueue orderQueue, CancellationToken cancellationToken)
    {
        await context.OrderQueue.AddAsync(orderQueue, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateStatusAsync(Guid id, OrderQueueStatus status, CancellationToken cancellationToken)
    {
        var orderQueue = await GetByIdAsync(id, cancellationToken);
        if (orderQueue != null)
        {
            orderQueue.Status = status;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}