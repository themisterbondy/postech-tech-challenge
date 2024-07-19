using FluentValidation;
using PosTech.MyFood.Features.Customers.Repository;
using PosTech.MyFood.WebApi.Common.Validation;
using PosTech.MyFood.WebApi.Features.Customers.Contracts;
using PosTech.MyFood.WebApi.Features.Customers.Entities;

namespace PosTech.MyFood.WebApi.Features.Customers.Commands;

public class CreateCustomer
{
    public class Command : IRequest<Result<CreateCustomerResponse>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
    }

    public class CreateCustomerValidator : AbstractValidator<Command>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithError(Error.Validation("Name", "Name is required."));

            RuleFor(x => x.Email)
                .NotEmpty().WithError(Error.Validation("Email", "Email is required."))
                .EmailAddress().WithError(Error.Validation("Email", "Email is invalid."));

            RuleFor(x => x.CPF)
                .NotEmpty().WithError(Error.Validation("CPF", "CPF is required."))
                .Matches("^[0-9]*$").WithError(Error.Validation("CPF", "CPF must contain only numbers."))
                .Length(11).WithError(Error.Validation("CPF", "CPF must have 11 characters."))
                .Must(GlobalValidations.BeAValidCPF).WithMessage("CPF is invalid.");
        }
    }

    public class CreateCustomerHandler(ICustomerRepository customerRepository)
        : IRequestHandler<Command, Result<CreateCustomerResponse>>
    {
        public async Task<Result<CreateCustomerResponse>> Handle(Command request,
            CancellationToken cancellationToken)
        {
            var existingCustomer = await customerRepository.GetByCPFAsync(request.CPF, cancellationToken);

            if (existingCustomer.IsSuccess)
                return Result.Failure<CreateCustomerResponse>(Error.Conflict("CreateCustomer.CreateCustomerHandler",
                    "Customer already exists."));

            var customer = await customerRepository.CreateAsync(
                Customer.Create(CustomerId.New(),
                    request.Name,
                    request.Email,
                    request.CPF),
                cancellationToken);

            if (customer.IsFailure)
                return Result.Failure<CreateCustomerResponse>(existingCustomer.Error);

            return new CreateCustomerResponse
            {
                Id = customer.Value.Id.Value,
                Name = customer.Value.Name,
                Email = customer.Value.Email,
                CPF = customer.Value.CPF
            };
        }
    }
}