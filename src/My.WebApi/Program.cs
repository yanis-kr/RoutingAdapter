using Serilog;
using My.WebApi;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatProvider: System.Globalization.CultureInfo.InvariantCulture)
    .CreateBootstrapLogger();

Log.Information("My API is starting");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(context.Configuration));
//{
//    loggerConfiguration
//         .WriteTo.Console().ReadFrom
//         .Configuration(context.Configuration);
//});

var app = builder
       .ConfigureServices()
       .ConfigurePipeline();

app.UseSerilogRequestLogging();

app.Run();

//required by WebApplicationFactory in Integration Tests
public partial class Program { }
