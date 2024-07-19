using Microsoft.AspNetCore.Mvc;
using PosTech.MyFood.Features.Customers.Queries;
using PosTech.MyFood.WebApi.Features.Customers.Commands;
using PosTech.MyFood.WebApi.Features.Customers.Contracts;

namespace PosTech.MyFood.WebApi.Features.Customers.Endpoints;

public class CustomersEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customers");

        group.MapPost("/", async ([FromBody] CustomerRequest request, [FromServices] IMediator mediator) =>
            {
                var Command = new CreateCustomer.Command
                {
                    Name = request.Name,
                    Email = request.Email,
                    CPF = request.CPF
                };

                var result = await mediator.Send(Command);

                return result.IsSuccess
                    ? Results.Created($"/Customers/{result.Value.CPF}", result.Value)
                    : result.ToProblemDetails();
            })
            .WithName("CreateCustomer")
            .Accepts<CustomerRequest>("application/json")
            .Produces<CustomerResponse>(201)
            .WithTags("Customers")
            .WithOpenApi();

        group.MapGet("/{cpf}", async ([FromQuery] string cpf, [FromServices] IMediator mediator) =>
            {
                var query = new GetCustomerByCpf.Query { CPF = cpf };
                var result = await mediator.Send(query);
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : result.ToProblemDetails();
            })
            .WithName("GetCustomerByCpf")
            .Produces<CustomerResponse>()
            .WithTags("Customers")
            .WithOpenApi();
    }
}