namespace PosTech.MyFood.WebApi.Features.Customers.Entities;

public record CustomerId(Guid Value)
{
    public static CustomerId New()
    {
        return new CustomerId(Guid.NewGuid());
    }
}