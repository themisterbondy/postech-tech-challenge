using PosTech.MyFood.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.Features.Orders.Contracts;

public class OrderDto
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public string? CustomerCpf { get; set; }
    public List<OrderItemDto> Items { get; set; }
}

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public decimal? UnitPrice { get; set; }
    public int Quantity { get; set; }
    public ProductCategory Category { get; set; } // Lanche, Acompanhamento, Bebida, Sobremesa
}