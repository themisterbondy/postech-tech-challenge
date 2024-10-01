using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Repositories;

namespace PosTech.MyFood.WebApi.Features.Orders.Services;

public class OrderQueueService(
    IOrderQueueRepository orderQueueRepository) : IOrderQueueService
{
    public async Task<Result<EnqueueOrderResponse>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var orderQueue = await orderQueueRepository.GetByIdAsync(id, cancellationToken);

        if (orderQueue == null)
            return Result.Failure<EnqueueOrderResponse>(Error.Failure("OrderQueueService.UpdateOrderStatusAsync",
                $"Order with id {id} not found."));

        return new EnqueueOrderResponse
        {
            Id = orderQueue.Id.Value,
            CreatedAt = orderQueue.CreatedAt,
            CustomerCpf = orderQueue.CustomerCpf,
            Status = orderQueue.Status,
            Items = orderQueue.Items.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId.Value,
                ProductName = item.ProductName,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                Category = item.Category
            }).ToList()
        };
    }

    public async Task<Result<EnqueueOrderResponse>> UpdateOrderStatusAsync(Guid id, OrderQueueStatus status,
        CancellationToken cancellationToken)
    {
        var orderQueue = await orderQueueRepository.GetByIdAsync(id, cancellationToken);

        if (orderQueue == null)
            return Result.Failure<EnqueueOrderResponse>(Error.Failure("OrderQueueService.UpdateOrderStatusAsync",
                $"Order with id {id} not found."));

        orderQueue.UpdateStatus(status);
        await orderQueueRepository.UpdateStatusAsync(id, status, cancellationToken);

        return new EnqueueOrderResponse
        {
            Id = orderQueue.Id.Value,
            CreatedAt = orderQueue.CreatedAt,
            CustomerCpf = orderQueue.CustomerCpf,
            Status = status,
            Items = orderQueue.Items.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId.Value,
                ProductName = item.ProductName,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                Category = item.Category
            }).ToList()
        };
    }
}