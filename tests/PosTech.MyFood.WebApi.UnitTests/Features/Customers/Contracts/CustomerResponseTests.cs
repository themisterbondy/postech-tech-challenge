using PosTech.MyFood.WebApi.Features.Customers.Contracts;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Customers.Contracts;

public class CustomerResponseTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        var id = Guid.NewGuid();
        var name = "John Doe";
        var email = "john.doe@example.com";
        var cpf = "12345678900";

        var response = new CustomerResponse { Id = id, Name = name, Email = email, Cpf = cpf };

        response.Id.Should().Be(id);
        response.Name.Should().Be(name);
        response.Email.Should().Be(email);
        response.Cpf.Should().Be(cpf);
    }

    [Fact]
    public void Constructor_SetsIdToNull_WhenIdIsNotProvided()
    {
        var name = "John Doe";
        var email = "john.doe@example.com";
        var cpf = "12345678900";

        var response = new CustomerResponse { Name = name, Email = email, Cpf = cpf };

        response.Id.Should().BeNull();
    }

    [Fact]
    public void Constructor_SetsNameToNull_WhenNameIsNotProvided()
    {
        var id = Guid.NewGuid();
        var email = "john.doe@example.com";
        var cpf = "12345678900";

        var response = new CustomerResponse { Id = id, Email = email, Cpf = cpf };

        response.Name.Should().BeNull();
    }

    [Fact]
    public void Constructor_SetsEmailToNull_WhenEmailIsNotProvided()
    {
        var id = Guid.NewGuid();
        var name = "John Doe";
        var cpf = "12345678900";

        var response = new CustomerResponse { Id = id, Name = name, Cpf = cpf };

        response.Email.Should().BeNull();
    }

    [Fact]
    public void Constructor_SetsCPFToNull_WhenCPFIsNotProvided()
    {
        var id = Guid.NewGuid();
        var name = "John Doe";
        var email = "john.doe@example.com";

        var response = new CustomerResponse { Id = id, Name = name, Email = email };

        response.Cpf.Should().BeNull();
    }
}