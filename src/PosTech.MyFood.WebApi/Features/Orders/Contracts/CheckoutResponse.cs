namespace PosTech.MyFood.WebApi.Features.Orders.Contracts;

public class CheckoutResponse
{
    public Guid OrderId { get; set; }
    public string CustomerId { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> Items { get; set; }
}