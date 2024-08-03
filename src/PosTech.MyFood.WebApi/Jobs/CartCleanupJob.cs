using PosTech.MyFood.Features.Carts.Repositories;
using Quartz;

namespace PosTech.MyFood.Jobs;

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