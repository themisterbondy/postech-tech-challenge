using PosTech.MyFood.WebApi.Features.Orders.Entities;

namespace PosTech.MyFood.WebApi.Features.Orders.Contracts;

public class EnqueueOrderResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CustomerCpf { get; set; }
    public OrderQueueStatus Status { get; set; }
    public List<OrderItemDto> Items { get; set; }
}

public class ListOrdersResponse
{
    public List<OrderDto> Orders { get; set; }
}