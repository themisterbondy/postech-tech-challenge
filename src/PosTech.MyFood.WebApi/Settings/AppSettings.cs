using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PosTech.MyFood.WebApi.Settings;

[ExcludeFromCodeCoverage]
public static class AppSettings
{
    public static IConfigurationRoot Configuration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, false)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                true)
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
            .AddEnvironmentVariables()
            .Build();
    }
}