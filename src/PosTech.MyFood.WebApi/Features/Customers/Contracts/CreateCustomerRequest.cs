namespace PosTech.MyFood.WebApi.Features.Customers.Contracts;

public class CreateCustomerRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
}