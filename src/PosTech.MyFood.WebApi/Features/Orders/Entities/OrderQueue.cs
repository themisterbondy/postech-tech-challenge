namespace PosTech.MyFood.WebApi.Features.Orders.Entities;

public class OrderQueue
{
    private OrderQueue(OrderId id, string? customerCpf, List<OrderItem> items, string transactionId,
        OrderQueueStatus status)
    {
        Id = id;
        CustomerCpf = customerCpf;
        Items = items;
        TransactionId = transactionId;
        Status = status;
    }

    private OrderQueue()
    {
    }

    public OrderId Id { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public OrderQueueStatus Status { get; set; }
    public string? CustomerCpf { get; set; }
    public List<OrderItem> Items { get; set; }

    public string? TransactionId { get; set; }

    public static OrderQueue Create(OrderId orderId, string? customerCpf, List<OrderItem> orderItems,
        string transactionId,
        OrderQueueStatus status)
    {
        return new OrderQueue(orderId, customerCpf, orderItems, transactionId, status);
    }
}