using PosTech.MyFood.WebApi.Features.Customers.Entities;

namespace PosTech.MyFood.WebApi.Features.Customers.Repositories;

public interface ICustomerRepository
{
    Task<Result<Customer>> CreateAsync(Customer? customer, CancellationToken cancellationToken);
    Task<Result<Customer>?> GetByCPFAsync(string? cpf, CancellationToken cancellationToken);
    Task<Result<Customer>?> GetByEmailAsync(string? email, CancellationToken cancellationToken);
}