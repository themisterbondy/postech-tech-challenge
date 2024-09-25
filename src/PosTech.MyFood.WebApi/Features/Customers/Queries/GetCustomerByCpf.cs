using FluentValidation;
using PosTech.MyFood.WebApi.Common.Validation;
using PosTech.MyFood.WebApi.Features.Customers.Contracts;
using PosTech.MyFood.WebApi.Features.Customers.Repositories;

namespace PosTech.MyFood.WebApi.Features.Customers.Queries;

public class GetCustomerByCpf
{
    public class Query : IRequest<Result<CustomerResponse>>
    {
        public required string Cpf { get; set; }
    }

    public class CreateCustomerValidator : AbstractValidator<Query>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty().WithError(Error.Validation("CPF", "CPF is required."))
                .Matches("^[0-9]*$").WithError(Error.Validation("CPF", "CPF must contain only numbers."))
                .Length(11).WithError(Error.Validation("CPF", "CPF must have 11 characters."))
                .Must(GlobalValidations.BeAValidCpf).WithMessage("CPF is invalid.");
        }
    }

    public class GetCustomerByCpfHandler(ICustomerRepository customerRepository)
        : IRequestHandler<Query, Result<CustomerResponse>>
    {
        public async Task<Result<CustomerResponse>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            var getCustomer = await customerRepository.GetByCpfAsync(request.Cpf, cancellationToken);

            if (getCustomer.IsFailure || getCustomer.Value == null)
                return Result.Failure<CustomerResponse>(Error.NotFound("GetCustomerByCpfHandler.Handle",
                    "Customer not found."));

            return new CustomerResponse
            {
                Id = getCustomer.Value.Id.Value,
                Name = getCustomer.Value.Name,
                Email = getCustomer.Value.Email,
                Cpf = getCustomer.Value.Cpf
            };
        }
    }
}