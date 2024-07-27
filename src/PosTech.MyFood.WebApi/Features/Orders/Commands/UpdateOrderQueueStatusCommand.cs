using FluentValidation;
using PosTech.MyFood.WebApi.Common.Validation;
using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Services;

namespace PosTech.MyFood.WebApi.Features.Orders.Commands;

public class UpdateOrderQueueStatusCommand
{
    public class Command : IRequest<Result<EnqueueOrderResponse>>
    {
        public Guid Id { get; set; }
        public OrderQueueStatus Status { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty().WithError(Error.Validation("Id", "Id is required."));
            RuleFor(x => x.Status).IsInEnum().WithError(Error.Validation("Status", "Status is invalid."));
        }
    }

    public class Handler(IOrderQueueService orderQueueService) : IRequestHandler<Command, Result<EnqueueOrderResponse>>
    {
        public async Task<Result<EnqueueOrderResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await orderQueueService.UpdateOrderStatusAsync(request.Id, request.Status, cancellationToken);
        }
    }
}