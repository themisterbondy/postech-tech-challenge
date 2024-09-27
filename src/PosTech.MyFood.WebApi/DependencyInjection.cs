using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using PosTech.MyFood.WebApi.Common;
using PosTech.MyFood.WebApi.Common.Behavior;
using PosTech.MyFood.WebApi.Features.Carts.Repositories;
using PosTech.MyFood.WebApi.Features.Carts.Services;
using PosTech.MyFood.WebApi.Features.Customers.Repositories;
using PosTech.MyFood.WebApi.Features.Customers.Services;
using PosTech.MyFood.WebApi.Features.Orders.Repositories;
using PosTech.MyFood.WebApi.Features.Orders.Services;
using PosTech.MyFood.WebApi.Features.Payments.Services;
using PosTech.MyFood.WebApi.Features.Products.Repositories;
using PosTech.MyFood.WebApi.Jobs;
using PosTech.MyFood.WebApi.Persistence;
using Quartz;
using Quartz.AspNetCore;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;

namespace PosTech.MyFood.WebApi;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    private static readonly Assembly Assembly = typeof(Program).Assembly;

    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatRConfiguration();
        services.AddSwaggerConfiguration();
        services.AddOpenTelemetryConfiguration();
        services.AddJsonOptions();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddUseHealthChecksConfiguration(configuration);
        services.AddValidatorsFromAssembly(Assembly);

        services.AddProblemDetails();
        services.AddCarter();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SQLConnection")));

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerServices, CustomerServices>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderQueueRepository, OrderQueueRepository>();
        services.AddScoped<IOrderQueueService, OrderQueueService>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IPaymentService, FakePaymentService>();
        services.AddJobs();


        return services;
    }

    private static IServiceCollection AddJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            var cartJobKey = new JobKey("CartCleanupJob");
            q.AddJob<CartCleanupJob>(opts => opts.WithIdentity(cartJobKey));
            q.AddTrigger(opts => opts
                .ForJob(cartJobKey)
                .WithIdentity("CartCleanupJob-trigger")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(5)
                    .RepeatForever())
                .StartAt(DateBuilder.FutureDate(5, IntervalUnit.Minute))
            );

            var orderJobKey = new JobKey("OrderCleanupJob");
            q.AddJob<OrderCleanupJob>(opts => opts.WithIdentity(orderJobKey));
            q.AddTrigger(opts => opts
                .ForJob(orderJobKey)
                .WithIdentity("OrderCleanupJob-trigger")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(30)
                    .RepeatForever())
                .StartAt(DateBuilder.FutureDate(5, IntervalUnit.Minute))
            );
        });

        services.AddQuartzServer(options => { options.WaitForJobsToComplete = true; });

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
                Title = "PosTech MyFood API"
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
                tracing.ConfigureResource(resource => resource
                    .AddService(serviceName)
                    .AddTelemetrySdk());
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

    public static IServiceCollection AddJsonOptions(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }
}