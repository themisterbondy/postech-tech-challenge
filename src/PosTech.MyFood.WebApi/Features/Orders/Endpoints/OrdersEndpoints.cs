using Microsoft.AspNetCore.Mvc;
using PosTech.MyFood.WebApi.Features.Orders.Commands;
using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Queries;

namespace PosTech.MyFood.WebApi.Features.Orders.Endpoints;

[ExcludeFromCodeCoverage]
public class OrdersEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders");

        group.MapPut("/{id:guid}/status",
                async (Guid id, [FromQuery] OrderQueueStatus status, IMediator mediator) =>
                {
                    var result = await mediator.Send(new UpdateOrderQueueStatusCommand.Command
                    {
                        Id = id,
                        Status = status
                    });
                    return result.IsSuccess
                        ? Results.Created($"/Order/{result.Value.OrderId}", result.Value)
                        : result.ToProblemDetails();
                })
            .WithName("UpdateOrderStatus")
            .Produces<EnqueueOrderResponse>(200)
            .WithTags("Orders")
            .WithOpenApi();

        group.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetOrderQueueById.Query { Id = id });
                return result.IsSuccess
                    ? Results.Created($"/order/{result.Value.OrderId}", result.Value)
                    : result.ToProblemDetails();
            })
            .WithName("GetOrder")
            .Produces<EnqueueOrderResponse>(200)
            .WithTags("Orders")
            .WithOpenApi();

        group.MapGet("/{transactionId}", async ([FromQuery]string transactionId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrderQueueByTransactionId.Query
                    { TransactionId = transactionId });
                return result.IsSuccess
                    ? Results.Created($"/Order/{result.Value.OrderId}", result.Value)
                    : result.ToProblemDetails();
            })
            .WithName("GetOrderByTransactionId")
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