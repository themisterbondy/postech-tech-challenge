using PosTech.MyFood.WebApi.Features.Customers.Contracts;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Customers.Contracts;

public class CustomerRequestTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        var name = "John Doe";
        var email = "john.doe@example.com";
        var cpf = "12345678900";

        var request = new CustomerRequest { Name = name, Email = email, Cpf = cpf };

        request.Name.Should().Be(name);
        request.Email.Should().Be(email);
        request.Cpf.Should().Be(cpf);
    }

    [Fact]
    public void Constructor_SetsNameToNull_WhenNameIsNotProvided()
    {
        var email = "john.doe@example.com";
        var cpf = "12345678900";

        var request = new CustomerRequest { Email = email, Cpf = cpf };

        request.Name.Should().BeNull();
    }

    [Fact]
    public void Constructor_SetsEmailToNull_WhenEmailIsNotProvided()
    {
        var name = "John Doe";
        var cpf = "12345678900";

        var request = new CustomerRequest { Name = name, Cpf = cpf };

        request.Email.Should().BeNull();
    }

    [Fact]
    public void Constructor_SetsCPFToNull_WhenCPFIsNotProvided()
    {
        var name = "John Doe";
        var email = "john.doe@example.com";

        var request = new CustomerRequest { Name = name, Email = email };

        request.Cpf.Should().BeNull();
    }
}