using PosTech.MyFood.WebApi.Features.Customers.Repositories;
using PosTech.MyFood.WebApi.UnitTests.Mocks;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Customers.Repositories;

public class CustomerRepositoryTests
{
    [Fact]
    public async Task CreateAsync_ReturnsFailure_WhenCustomerIsNull()
    {
        var context = ApplicationDbContextMock.Create();
        var repository = new CustomerRepository(context);

        var result = await repository.CreateAsync(null, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("CustomerRepository.CreateAsync");
        result.Error.Message.Should().Be("Customer is null.");
    }


    [Fact]
    public async Task CreateAsync_ReturnsSuccess_WhenCustomerIsValid()
    {
        var context = ApplicationDbContextMock.Create();
        var customer = CustomerMocks.GenerateValidCustomer();
        var repository = new CustomerRepository(context);

        var result = await repository.CreateAsync(customer, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(customer);
    }

    [Fact]
    public async Task GetByCPFAsync_ReturnsFailure_WhenCPFIsNullOrEmpty()
    {
        var context = ApplicationDbContextMock.Create();
        var repository = new CustomerRepository(context);

        var result = await repository.GetByCpfAsync(null, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("CustomerRepository.GetByCPFAsync");
        result.Error.Message.Should().Be("CPF is null.");
    }

    [Fact]
    public async Task GetByCPFAsync_ReturnsSuccess_WhenCustomerExists()
    {
        var context = ApplicationDbContextMock.Create();
        var customer = CustomerMocks.GenerateValidCustomer();
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
        var repository = new CustomerRepository(context);

        var result = await repository.GetByCpfAsync(customer.Cpf, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(customer);
    }

    [Fact]
    public async Task GetByEmailAsync_ReturnsFailure_WhenEmailIsNullOrEmpty()
    {
        var context = ApplicationDbContextMock.Create();
        var repository = new CustomerRepository(context);

        var result = await repository.GetByEmailAsync(null, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("CustomerRepository.GetByEmailAsync");
        result.Error.Message.Should().Be("Email is null.");
    }

    [Fact]
    public async Task GetByEmailAsync_ReturnsSuccess_WhenCustomerExists()
    {
        var context = ApplicationDbContextMock.Create();
        var customer = CustomerMocks.GenerateValidCustomer();
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
        var repository = new CustomerRepository(context);

        var result = await repository.GetByEmailAsync(customer.Email, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(customer);
    }
}