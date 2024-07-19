namespace PosTech.MyFood.Features.Products.Entities;

public record ProductId(Guid Value)
{
    public static ProductId New()
    {
        return new ProductId(Guid.NewGuid());
    }
}