using Carter;
using DocHub.DocumentStorage.WebApi.Common;
using PosTech.MyFood;
using PosTech.MyFood.WebApi.Common.Middleware;
using PosTech.MyFood.WebApi.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = AppSettings.Configuration();
builder.Services.AddWebApi(configuration);
builder.Services.AddSerilogConfiguration(builder, configuration);

var app = builder.Build();
app.UseHealthChecksConfiguration();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseSerilogRequestLogging();
app.UseMiddleware<RequestContextLoggingMiddleware>();
app.MapCarter();
app.Run();