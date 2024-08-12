using PosTech.MyFood.WebApi.Features.Carts.Repositories;
using Quartz;

namespace PosTech.MyFood.WebApi.Jobs;

[ExcludeFromCodeCoverage]
public class CartCleanupJob(ICartRepository cartRepository, ILogger<CartCleanupJob> logger)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("CartCleanupJob started.");

        var threshold = DateTime.UtcNow.AddMinutes(-15);
        await cartRepository.DeleteCartsOlderThanAsync(threshold);

        logger.LogInformation("CartCleanupJob finished.");
    }
}