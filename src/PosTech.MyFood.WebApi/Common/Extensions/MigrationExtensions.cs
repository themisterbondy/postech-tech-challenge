using Microsoft.EntityFrameworkCore;
using PosTech.MyFood.WebApi.Persistence;

namespace PosTech.MyFood.WebApi.Common.Extensions;

[ExcludeFromCodeCoverage]
public static class MigrationExtensions
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}