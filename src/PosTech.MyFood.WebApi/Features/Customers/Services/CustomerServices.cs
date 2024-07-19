using PosTech.MyFood.WebApi.Features.Customers.Repositories;

namespace PosTech.MyFood.Features.Customers.Services;

public class CustomerServices(ICustomerRepository customerRepository) : ICustomerServices
{
    public async Task<Result> IsUniqueCustomer(string email, string cpf, CancellationToken cancellationToken)
    {
        var existingCustomerByEmail = await customerRepository.GetByEmailAsync(email, cancellationToken);

        if (existingCustomerByEmail != null)
            return Result.Failure(Error.Conflict("CustomerServices.IsUniqueCustomer",
                "Customer already exists with this email."));

        var existingCustomerByCPF = await customerRepository.GetByCPFAsync(cpf, cancellationToken);

        if (existingCustomerByCPF != null)
            return Result.Failure(Error.Conflict("CustomerServices.IsUniqueCustomer",
                "Customer already exists with this CPF."));

        return Result.Success();
    }
}