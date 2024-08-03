namespace PosTech.MyFood.Features.Carts.Entities;

public record CartId(Guid Value)
{
    public static CartId New()
    {
        return new CartId(Guid.NewGuid());
    }
}