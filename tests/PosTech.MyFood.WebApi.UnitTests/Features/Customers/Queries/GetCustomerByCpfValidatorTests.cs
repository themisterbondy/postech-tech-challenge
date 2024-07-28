using PosTech.MyFood.WebApi.Features.Customers.Contracts;
using PosTech.MyFood.WebApi.Features.Customers.Entities;
using PosTech.MyFood.WebApi.Features.Customers.Queries;
using PosTech.MyFood.WebApi.Features.Customers.Repositories;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Customers.Queries;

public class GetCustomerByCpfValidatorTests
{
    private readonly GetCustomerByCpf.CreateCustomerValidator _validator = new();

    [Fact]
    public void Validator_ShouldHaveError_WhenCPFIsEmpty()
    {
        var query = new GetCustomerByCpf.Query { CPF = "" };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.CPF);
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenCPFIsNot11Characters()
    {
        var query = new GetCustomerByCpf.Query { CPF = "123456789" };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.CPF);
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenCPFIsInvalid()
    {
        var query = new GetCustomerByCpf.Query { CPF = "12345678900" };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.CPF).WithErrorMessage("CPF is invalid.");
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenCPFIsValid()
    {
        var query = new GetCustomerByCpf.Query { CPF = "12345678909" }; // Assuming this is a valid CPF
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }
}

public class GetCustomerByCpfHandlerTests
{
    private readonly ICustomerRepository _customerRepository = Substitute.For<ICustomerRepository>();
    private readonly GetCustomerByCpf.GetCustomerByCpfHandler _handler;

    public GetCustomerByCpfHandlerTests()
    {
        _handler = new GetCustomerByCpf.GetCustomerByCpfHandler(_customerRepository);
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenCustomerNotFound()
    {
        var query = new GetCustomerByCpf.Query { CPF = "12345678909" };
        _customerRepository.GetByCPFAsync(query.CPF, CancellationToken.None).Returns(
            Result.Failure<Customer>(Error.Failure("CustomerRepository.GetByCPFAsync", "CPF is null.")));

        var result = await _handler.Handle(query, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be("Customer not found.");
    }

    [Fact]
    public async Task Handle_ReturnsSuccess_WhenCustomerIsFound()
    {
        var query = new GetCustomerByCpf.Query { CPF = "12345678909" };
        var customer = Customer.Create(CustomerId.New(), "John Doe", "", query.CPF);
        _customerRepository.GetByCPFAsync(query.CPF, CancellationToken.None).Returns(Result.Success(customer));

        var result = await _handler.Handle(query, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new CustomerResponse
        {
            Id = customer.Id.Value,
            Name = customer.Name,
            Email = customer.Email,
            CPF = customer.CPF
        });
    }
}