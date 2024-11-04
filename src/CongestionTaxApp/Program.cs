using CongestionTaxApp.Configurations;
using CongestionTaxApp.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Set up Serilog for logging
ConfigureLogging();

ServiceRegistry.AddApplicationServices(builder.Services);

var app = builder.Build();

ConfigureMiddleware(app);

if (app.Environment.IsDevelopment())
{
    ConfigureSwagger(app);
}

await app.RunAsync();

void ConfigureLogging()
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .CreateLogger();
    builder.Host.UseSerilog();
}

void ConfigureMiddleware(WebApplication app)
{
    app.UseHttpsRedirection();
    app.UseMiddleware<RequestLoggingMiddleware>();
    EndpointMapper.MapAllEndpoints(app);
}

void ConfigureSwagger(WebApplication app)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

