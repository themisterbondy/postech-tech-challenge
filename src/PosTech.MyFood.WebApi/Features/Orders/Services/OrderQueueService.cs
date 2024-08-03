using PosTech.MyFood.Features.Products.Repositories;
using PosTech.MyFood.WebApi.Features.Customers.Repositories;
using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Repositories;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.Features.Orders.Services;

public class OrderQueueService(
    IOrderQueueRepository orderQueueRepository,
    ICustomerRepository customerRepository,
    IProductRepository productRepository) : IOrderQueueService
{
    public async Task<Result<EnqueueOrderResponse>> EnqueueOrderAsync(EnqueueOrderRequest request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.CustomerCpf))
        {
            var customer = await customerRepository.GetByCPFAsync(request.CustomerCpf, cancellationToken);
            if (customer == null)
                return Result.Failure<EnqueueOrderResponse>(Error.Failure("OrderQueueService.EnqueueOrderAsync",
                    $"Customer with cpf {request.CustomerCpf} not found."));
        }

        var orderQueue = OrderQueue.Create(
            OrderId.New(),
            DateTime.UtcNow,
            request.CustomerCpf,
            []
        );

        foreach (var item in request.Items)
        {
            var product =
                await productRepository.FindByIdAsync(new ProductId(item.ProductId), cancellationToken);

            if (product == null)
                return Result.Failure<EnqueueOrderResponse>(Error.Failure("OrderQueueService.EnqueueOrderAsync",
                    $"Product with id {item.ProductId} not found."));

            var orderItem = OrderItem.Create(
                OrderItemId.New(),
                orderQueue.Id,
                product.Id,
                product.Name,
                product.Price,
                item.Quantity,
                product.Category
            );

            orderQueue.Items.Add(orderItem);
        }

        await orderQueueRepository.AddAsync(orderQueue, cancellationToken);

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