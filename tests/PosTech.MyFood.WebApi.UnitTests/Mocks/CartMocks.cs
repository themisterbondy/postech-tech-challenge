using PosTech.MyFood.WebApi.Features.Carts.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Mocks;

public static class CartMocks
{
    public static Cart GenerateValidCart()
    {
        return Cart.Create(CartId.New(), "12345678901");
    }

    public static Cart GenerateOldCart()
    {
        var cart = Cart.Create(CartId.New(), "12345678901");
        cart.CreatedAt = DateTime.UtcNow.AddDays(-31); // Ensure the cart is older than 30 days
        return cart;
    }
}