namespace PosTech.MyFood.WebApi.Features.Carts.Entities;

public class Cart
{
    private Cart(CartId id, string customerId)
    {
        Id = id;
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
    }

    public CartId Id { get; set; }
    public string CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<CartItem> Items { get; set; } = new();

    public static Cart Create(CartId id, string customerId)
    {
        return new Cart(id, customerId);
    }

    public void AddItem(CartItem item)
    {
        Items.Add(item);
    }

    public void RemoveItem(CartItemId itemId)
    {
        Items.RemoveAll(x => x.Id == itemId);
    }
}