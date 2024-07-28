namespace PosTech.MyFood.WebApi.Features.Orders.Entities;

public class OrderQueue
{
    private OrderQueue(OrderId id, DateTime createdAt, OrderQueueStatus status, string? customerCpf,
        List<OrderItem> items)
    {
        Id = id;
        CreatedAt = createdAt;
        Status = status;
        CustomerCpf = customerCpf;
        Items = items;
    }

    private OrderQueue()
    {
    }

    public OrderId Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderQueueStatus Status { get; set; }
    public string? CustomerCpf { get; set; }
    public List<OrderItem> Items { get; set; }

    public static OrderQueue Create(OrderId id, DateTime createdAt, string? customerCpf,
        List<OrderItem> items)
    {
        return new OrderQueue(id, createdAt, OrderQueueStatus.Received, customerCpf, items);
    }
}