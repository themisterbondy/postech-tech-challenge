using PosTech.MyFood.WebApi.Features.Customers.Entities;
using PosTech.MyFood.WebApi.Features.Customers.Repositories;
using PosTech.MyFood.WebApi.Features.Customers.Services;
using PosTech.MyFood.WebApi.UnitTests.Mocks;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Customers.Services;

public class CustomerServicesTests
{
    [Fact]
    public async Task IsUniqueCustomer_ReturnsFailure_WhenEmailIsNullOrEmpty()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();
        var service = new CustomerServices(customerRepository);

        // Act
        var result = await service.IsUniqueCustomer(null, "12345678900", CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("CustomerServices.IsUniqueCustomer");
        result.Error.Message.Should().Be("Email and CPF are required.");
    }

    [Fact]
    public async Task IsUniqueCustomer_ReturnsFailure_WhenCPFIsNullOrEmpty()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();
        var service = new CustomerServices(customerRepository);

        // Act
        var result = await service.IsUniqueCustomer("test@example.com", null, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("CustomerServices.IsUniqueCustomer");
        result.Error.Message.Should().Be("Email and CPF are required.");
    }

    [Fact]
    public async Task IsUniqueCustomer_ReturnsFailure_WhenCustomerExistsWithEmail()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();
        customerRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Failure<Customer>(Error.Conflict("CustomerServices.IsUniqueCustomer",
                "Customer already exists with this email."))));
        var service = new CustomerServices(customerRepository);

        // Act
        var result = await service.IsUniqueCustomer("test@example.com", "12345678900", CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("CustomerServices.IsUniqueCustomer");
        result.Error.Message.Should().Be("Customer already exists with this email.");
    }

    [Fact]
    public async Task IsUniqueCustomer_ReturnsFailure_WhenCustomerExistsWithCPF()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();
        customerRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Success(CustomerMocks.GenerateValidCustomer())));
        customerRepository.GetByCPFAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Failure<Customer>(Error.Conflict("CustomerServices.IsUniqueCustomer",
                "Customer already exists with this CPF."))));
        var service = new CustomerServices(customerRepository);

        // Act
        var result = await service.IsUniqueCustomer("test@example.com", "12345678900", CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("CustomerServices.IsUniqueCustomer");
        result.Error.Message.Should().Be("Customer already exists with this CPF.");
    }

    [Fact]
    public async Task IsUniqueCustomer_ReturnsSuccess_WhenCustomerIsUnique()
    {
        // Arrange
        var customerRepository = Substitute.For<ICustomerRepository>();
        customerRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Success(CustomerMocks.GenerateValidCustomer())));
        customerRepository.GetByCPFAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Success(CustomerMocks.GenerateValidCustomer())));
        var service = new CustomerServices(customerRepository);

        // Act
        var result = await service.IsUniqueCustomer("test@example.com", "12345678900", CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}