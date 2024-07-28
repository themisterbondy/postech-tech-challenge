using Bogus;
using Bogus.Extensions.Brazil;
using PosTech.MyFood.WebApi.Features.Customers.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Mocks;

public static class CustomerMocks
{
    public static Customer GenerateValidCustomer()
    {
        var validCustomerFaker = new Faker<Customer>()
            .CustomInstantiator(f => Customer.Create(new CustomerId(Guid.NewGuid()),
                f.Person.FullName,
                f.Internet.Email(),
                f.Person.Cpf()))
            .RuleFor(u => u.Id, (f, u) => u.Id)
            .RuleFor(u => u.Name, (f, u) => u.Name)
            .RuleFor(u => u.Email, (f, u) => u.Email)
            .RuleFor(u => u.CPF, (f, u) => u.CPF);

        return validCustomerFaker.Generate();
    }

    public static Customer GenerateInvalidCustomer()
    {
        var invalidCustomerFaker = new Faker<Customer>()
            .RuleFor(u => u.Id, f => new CustomerId(Guid.NewGuid()))
            .RuleFor(u => u.Name, f => f.Random.AlphaNumeric(1)) // Name too short
            .RuleFor(u => u.Email, f => f.Random.Word()) // Invalid email format
            .RuleFor(u => u.CPF, f => f.Random.Word()); // Invalid CPF format

        return invalidCustomerFaker.Generate();
    }
}