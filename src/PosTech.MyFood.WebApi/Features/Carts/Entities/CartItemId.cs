namespace PosTech.MyFood.WebApi.Features.Carts.Entities;

public record CartItemId(Guid Value)
{
    public static CartItemId New()
    {
        return new CartItemId(Guid.NewGuid());
    }
}