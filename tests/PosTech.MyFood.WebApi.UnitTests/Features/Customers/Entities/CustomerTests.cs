using PosTech.MyFood.WebApi.Features.Customers.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Customers.Entities;

public class CustomerTests
{
    [Fact]
    public void CustomerId_ShouldInitializeCorrectly_WhenValidGuidIsProvided()
    {
        var guid = Guid.NewGuid();
        var customerId = new CustomerId(guid);

        customerId.Should().NotBeNull();
        customerId.Value.Should().Be(guid);
    }

    [Fact]
    public void CustomerId_ShouldGenerateNewGuid_WhenNewIsCalled()
    {
        var customerId = CustomerId.New();

        customerId.Should().NotBeNull();
        customerId.Value.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Customer_ShouldInitializeCorrectly_WhenValidParameters()
    {
        var id = new CustomerId(Guid.NewGuid());
        var name = "Test Customer";
        var email = "test@example.com";
        var cpf = "12345678901";

        var customer = Customer.Create(id, name, email, cpf);

        customer.Should().NotBeNull();
        customer.Id.Should().Be(id);
        customer.Name.Should().Be(name);
        customer.Email.Should().Be(email);
        customer.CPF.Should().Be(cpf);
    }

    [Fact]
    public void Customer_ShouldInitializeCorrectly_WhenEmailIsNull()
    {
        var id = new CustomerId(Guid.NewGuid());
        var name = "Test Customer";
        var cpf = "12345678901";

        var customer = Customer.Create(id, name, null, cpf);

        customer.Should().NotBeNull();
        customer.Id.Should().Be(id);
        customer.Name.Should().Be(name);
        customer.Email.Should().BeNull();
        customer.CPF.Should().Be(cpf);
    }

    [Fact]
    public void Customer_ShouldInitializeCorrectly_WhenCPFIsNull()
    {
        var id = new CustomerId(Guid.NewGuid());
        var name = "Test Customer";
        var email = "test@example.com";

        var customer = Customer.Create(id, name, email, null);

        customer.Should().NotBeNull();
        customer.Id.Should().Be(id);
        customer.Name.Should().Be(name);
        customer.Email.Should().Be(email);
        customer.CPF.Should().BeNull();
    }

    [Fact]
    public void Customer_ShouldInitializeCorrectly_WhenNameIsNull()
    {
        var id = new CustomerId(Guid.NewGuid());
        var email = "test@example.com";
        var cpf = "12345678901";

        var customer = Customer.Create(id, null, email, cpf);

        customer.Should().NotBeNull();
        customer.Id.Should().Be(id);
        customer.Name.Should().BeNull();
        customer.Email.Should().Be(email);
        customer.CPF.Should().Be(cpf);
    }
}