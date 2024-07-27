using Microsoft.AspNetCore.Mvc;
using PosTech.MyFood.WebApi.Features.Orders.Commands;
using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Queries;

namespace PosTech.MyFood.WebApi.Features.Orders.Endpoints;

public class OrdersEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders");

        group.MapPost("/",
                async (EnqueueOrderRequest request, ISender sender) =>
                {
                    var command = new CreateOrderCommand.Command
                    {
                        CustomerCpf = request.CustomerCpf,
                        Items = request.Items
                    };
                    var result = await sender.Send(command);
                    return result.IsSuccess
                        ? Results.Created($"/Order/{result.Value.Id}", result.Value)
                        : result.ToProblemDetails();
                })
            .WithName("CreateOrder")
            .Accepts<EnqueueOrderRequest>("application/json")
            .Produces<EnqueueOrderResponse>(201)
            .WithTags("Orders")
            .WithOpenApi();

        group.MapPut("/{id}/status",
                async ([FromQuery] Guid id, [FromQuery] OrderQueueStatus Status, IMediator mediator) =>
                {
                    var result = await mediator.Send(new UpdateOrderQueueStatusCommand.Command
                    {
                        Id = id,
                        Status = Status
                    });
                    return result.IsSuccess
                        ? Results.Created($"/Order/{result.Value.Id}", result.Value)
                        : result.ToProblemDetails();
                })
            .WithName("UpdateOrderStatus")
            .Produces<EnqueueOrderResponse>(200)
            .WithTags("Orders")
            .WithOpenApi();

        group.MapGet("/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetOrderQueueById.Query { Id = id });
                return result.IsSuccess
                    ? Results.Created($"/Order/{result.Value.Id}", result.Value)
                    : result.ToProblemDetails();
            })
            .WithName("GetOrder")
            .Produces<EnqueueOrderResponse>(200)
            .WithTags("Orders")
            .WithOpenApi();

        group.MapGet("/", async (ISender sender) =>
            {
                var result = await sender.Send(new ListOrders.Query());
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : result.ToProblemDetails();
            })
            .WithName("ListOrders")
            .Produces<ListOrdersResponse>(200)
            .WithTags("Orders")
            .WithOpenApi();
    }
}