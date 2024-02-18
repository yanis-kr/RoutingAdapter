using My.WebApi;
using Serilog;

//set pre-startup logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatProvider: System.Globalization.CultureInfo.InvariantCulture)
    .CreateLogger();
Log.Information("My API is starting");

var builder = WebApplication.CreateBuilder(args);

var app = builder
       .ConfigureServices()
       .ConfigurePipeline();



app.Run();

//required by WebApplicationFactory in Integration Tests
public partial class Program { }
