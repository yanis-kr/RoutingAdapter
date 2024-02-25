using Serilog;
using LoggerApp;
using LoggerApp.Models;
using LoggerApp.Handlers;
using LoggerApp.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for logging
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();

// Register typed HTTP client
builder.Services.AddHttpClient<HttpBinClient>(client =>
{
    //will use a simple HTTP Request & Response Service. See https://httpbin.org/#/ 
    client.BaseAddress = new Uri("https://httpbin.org/");
});

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
// Add Swagger generation with XML comments

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(xmlPath);
});

 var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseSerilogRequestLogging(); // Enable request logging

app.MapHealthChecks("/health");

// Set minimal API endpoints
// *************************
// Delegate with a method body for potentially complex logic.

/// <summary>
/// Sends GET request to external API https://httpbin.org/ and returns it's echo response
/// </summary>
app.MapGet("/api/echo", async (HttpBinClient client) =>
{
    return await client.GetAnythingAsync();
});

/// <summary>
/// Sends POST request to external API https://httpbin.org/ with sample DTO and returns it's echo response
/// </summary>
// Delegate as a single expression for simple operations.
app.MapPost("/api/echo", async (MyDto dto, HttpBinClient client) =>
    await MyDtoHandler.HandlePostAsync(dto, client)); // Directly returns a method call.

app.Run();
