namespace PosTech.MyFood.WebApi.Features.Orders.Entities;

public record OrderItemId(Guid Value)
{
    public static OrderItemId New()
    {
        return new OrderItemId(Guid.NewGuid());
    }
}