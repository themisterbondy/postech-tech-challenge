using PosTech.MyFood.WebApi.Features.Customers.Repositories;

namespace PosTech.MyFood.WebApi.Features.Customers.Services;

public class CustomerServices(ICustomerRepository customerRepository) : ICustomerServices
{
    public async Task<Result> IsUniqueCustomer(string? email, string? cpf, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(cpf))
            return Result.Failure(Error.Validation("CustomerServices.IsUniqueCustomer",
                "Email and CPF are required."));

        var existingCustomerByEmail = await customerRepository.GetByEmailAsync(email, cancellationToken);

        if (existingCustomerByEmail.IsFailure)
            return Result.Failure(Error.Conflict("CustomerServices.IsUniqueCustomer",
                "Customer already exists with this email."));

        var existingCustomerByCpf = await customerRepository.GetByCpfAsync(cpf, cancellationToken);

        if (existingCustomerByCpf.IsFailure)
            return Result.Failure(Error.Conflict("CustomerServices.IsUniqueCustomer",
                "Customer already exists with this CPF."));

        return Result.Success();
    }
}