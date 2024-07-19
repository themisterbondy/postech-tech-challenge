using PosTech.MyFood.WebApi.Features.Customers.Entities;

namespace PosTech.MyFood.WebApi.Features.Customers.Repositories;

public interface ICustomerRepository
{
    Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken);
    Task<Customer> GetByCPFAsync(string cpf, CancellationToken cancellationToken);
    Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken);
}