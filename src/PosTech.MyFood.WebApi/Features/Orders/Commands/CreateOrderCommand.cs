using FluentValidation;
using PosTech.MyFood.WebApi.Common.Validation;
using PosTech.MyFood.WebApi.Features.Orders.Contracts;
using PosTech.MyFood.WebApi.Features.Orders.Services;

namespace PosTech.MyFood.WebApi.Features.Orders.Commands;

public class CreateOrderCommand
{
    public class Command : IRequest<Result<EnqueueOrderResponse>>
    {
        public string? CustomerCpf { get; set; } = string.Empty;
        public List<OrderItemRequest> Items { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(x => x.ProductId)
                    .NotEmpty().WithError(Error.Validation("ProductId", "ProductId is required."));
                items.RuleFor(x => x.Quantity)
                    .GreaterThan(0).WithError(Error.Validation("Quantity", "Quantity must be greater than 0."));
            });

            When(x => !string.IsNullOrEmpty(x.CustomerCpf),
                () =>
                {
                    RuleFor(x => x.CustomerCpf).Matches(@"^\d{11}$")
                        .WithError(Error.Validation("CustomerCpf", "CustomerCpf must have 11 digits."));
                });
        }
    }

    public class Handler(IOrderQueueService orderQueueService)
        : IRequestHandler<Command, Result<EnqueueOrderResponse>>
    {
        public async Task<Result<EnqueueOrderResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var orderRequest = new EnqueueOrderRequest
            {
                CustomerCpf = request.CustomerCpf,
                Items = request.Items
            };

            return await orderQueueService.EnqueueOrderAsync(orderRequest, cancellationToken);
        }
    }
}