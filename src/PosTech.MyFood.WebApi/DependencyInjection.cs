using System.Reflection;
using Carter;
using DocHub.DocumentStorage.WebApi.Common;
using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using PosTech.MyFood.WebApi.Common.Behavior;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;

namespace PosTech.MyFood;

public static class DependencyInjection
{
    private static readonly Assembly Assembly = typeof(Program).Assembly;

    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatRConfiguration();
        services.AddSwaggerConfiguration();
        services.AddOpenTelemetryConfiguration();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddUseHealthChecksConfiguration(configuration);
        services.AddValidatorsFromAssembly(Assembly);

        services.AddProblemDetails();
        services.AddCarter();

        return services;
    }

    private static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        return services;
    }

    private static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Description = "PosTech MyFood API",
                Version = "v1",
                Title = "DocHub PosTech MyFood API"
            });
        });

        return services;
    }

    private static IServiceCollection AddOpenTelemetryConfiguration(this IServiceCollection services)
    {
        var serviceName = Assembly.GetName().Name;
        services.AddOpenTelemetry()
            .ConfigureResource(resourceBuilder => resourceBuilder.AddService(serviceName!))
            .WithTracing(tracing =>
            {
                tracing.AddSource(serviceName!);
                tracing.AddAspNetCoreInstrumentation();
                tracing.AddEntityFrameworkCoreInstrumentation();
                tracing.AddHttpClientInstrumentation(builder =>
                {
                    builder.EnrichWithHttpRequestMessage = (activity, message) =>
                    {
                        activity.SetTag("http.request_content", message.Content?.ReadAsStringAsync().Result);
                    };
                    builder.EnrichWithHttpResponseMessage = (activity, message) =>
                    {
                        activity.SetTag("http.response_content", message.Content?.ReadAsStringAsync().Result);
                    };
                });

                tracing.AddOtlpExporter();
            });

        return services;
    }

    public static IServiceCollection AddSerilogConfiguration(this IServiceCollection services,
        WebApplicationBuilder builder, IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var applicationName =
            $"{Assembly.GetName().Name?.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}";

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", applicationName)
            .Enrich.WithCorrelationId()
            .Enrich.WithExceptionDetails()
            .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Host.UseSerilog(Log.Logger, true);

        return services;
    }
}