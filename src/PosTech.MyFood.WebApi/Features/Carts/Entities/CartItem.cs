using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.Features.Carts.Entities;

public class CartItem
{
    private CartItem(CartItemId id, ProductId productId, string productName, decimal unitPrice, int quantity)
    {
        Id = id;
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    private CartItem()
    {
    }

    public CartItemId Id { get; set; }
    public ProductId ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public CartId CartId { get; set; }

    public static CartItem Create(CartItemId id, ProductId productId, string productName, decimal unitPrice,
        int quantity)
    {
        return new CartItem(id, productId, productName, unitPrice, quantity);
    }
}