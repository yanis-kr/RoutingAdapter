using System.Reflection;
using LoggerApp;
using LoggerApp.Middleware;
using Serilog;

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
app.MapApiEndpoints();

app.Run();
