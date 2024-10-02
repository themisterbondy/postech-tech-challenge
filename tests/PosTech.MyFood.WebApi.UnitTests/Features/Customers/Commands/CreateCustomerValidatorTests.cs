using PosTech.MyFood.WebApi.Features.Customers.Commands;
using PosTech.MyFood.WebApi.Features.Customers.Contracts;
using PosTech.MyFood.WebApi.Features.Customers.Entities;
using PosTech.MyFood.WebApi.Features.Customers.Repositories;
using PosTech.MyFood.WebApi.Features.Customers.Services;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Customers.Commands;

public class CreateCustomerValidatorTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerServices _customerServices;
    private readonly CreateCustomer.CreateCustomerHandler _handler;
    private readonly CreateCustomer.CreateCustomerValidator _validator;

    public CreateCustomerValidatorTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _customerServices = Substitute.For<ICustomerServices>();
        _handler = new CreateCustomer.CreateCustomerHandler(_customerRepository, _customerServices);
        _validator = new CreateCustomer.CreateCustomerValidator();
    }


    [Fact]
    public void Validator_ShouldHaveError_WhenCPFIsEmpty()
    {
        var command = new CreateCustomer.Command { Cpf = string.Empty };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Cpf);
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenCPFIsInvalid()
    {
        var command = new CreateCustomer.Command { Cpf = "123" }; // Invalid length
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Cpf);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenCustomerIsCreated()
    {
        // Arrange
        var command = new CreateCustomer.Command
        {
            Name = "Test Customer",
            Email = "test@example.com",
            Cpf = "12345678901"
        };

        _customerServices.IsUniqueCustomer(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Result.Success(true));

        var customer = Customer.Create(CustomerId.New(), command.Name, command.Email, command.Cpf);
        _customerRepository.CreateAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>())
            .Returns(Result.Success(customer));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new CustomerResponse
        {
            Id = customer.Id.Value,
            Name = customer.Name,
            Email = customer.Email,
            Cpf = customer.Cpf
        });
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenCustomerIsNotUnique()
    {
        // Arrange
        var command = new CreateCustomer.Command
        {
            Name = "Test Customer",
            Email = "test@example.com",
            Cpf = "12345678901"
        };

        _customerServices.IsUniqueCustomer(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Result.Failure<bool>(Error.Validation("Customer", "Customer already exists.")));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be("Customer");
        result.Error.Message.Should().Be("Customer already exists.");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenCreationFails()
    {
        // Arrange
        var command = new CreateCustomer.Command
        {
            Name = "Test Customer",
            Email = "test@example.com",
            Cpf = "12345678901"
        };

        _customerServices.IsUniqueCustomer(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Result.Success(true));

        _customerRepository.CreateAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>())
            .Returns(Result.Failure<Customer>(Error.Validation("Customer", "Failed to create customer.")));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be("Customer");
    }
}