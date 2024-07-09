using PosTech.MyFood;
using PosTech.MyFood.WebApi.Settings;

var builder = WebApplication.CreateBuilder(args);
{
    var configuration = AppSettings.Configuration();
    builder.Services.AddWebApi(configuration);
    builder.Services.AddSerilogConfiguration(builder, configuration);
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.Run();
}