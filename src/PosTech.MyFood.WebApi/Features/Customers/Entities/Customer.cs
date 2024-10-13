namespace PosTech.MyFood.WebApi.Features.Customers.Entities;

public class Customer
{
    private Customer(CustomerId id, string name, string email, string cpf)
    {
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }

    public CustomerId Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }

    public static Customer Create(CustomerId id, string name, string email, string cpf)
    {
        return new Customer(id, name, email, cpf);
    }
}