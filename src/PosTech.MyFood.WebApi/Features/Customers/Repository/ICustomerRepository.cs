using PosTech.MyFood.WebApi.Features.Customers.Entities;

namespace PosTech.MyFood.Features.Customers.Repository;

public interface ICustomerRepository
{
    Task<Result<Customer>> CreateAsync(Customer customer, CancellationToken cancellationToken);
    Task<Result<Customer>> GetByCPFAsync(string cpf, CancellationToken cancellationToken);
}