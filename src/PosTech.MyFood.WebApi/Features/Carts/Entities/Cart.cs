namespace PosTech.MyFood.Features.Carts.Entities;

public class Cart
{
    private Cart(CartId id, string customerCpf)
    {
        Id = id;
        CustomerCpf = customerCpf;
    }

    public CartId Id { get; set; }
    public string CustomerCpf { get; set; }
    public List<CartItem> Items { get; set; } = [];

    public static Cart Create(CartId id, string customerCpf)
    {
        return new Cart(id, customerCpf);
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