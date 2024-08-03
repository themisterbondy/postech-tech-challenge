namespace PosTech.MyFood.Features.Carts.Contracts;

public class CartRequest
{
    public string CustomerCpf { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CartItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}

public class CartResponse
{
    public Guid Id { get; set; }
    public string CustomerCpf { get; set; }
    public List<CartItemDto> Items { get; set; }
}