namespace PosTech.MyFood.WebApi.Features.Customers.Contracts;

public class CreateCustomerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
}