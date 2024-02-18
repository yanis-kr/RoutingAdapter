//using Microsoft.AspNetCore.Http;
//using Moq;
//using My.WebApi.Middleware;
//using Serilog;
//using Serilog.Core;
//using Serilog.Events;
//using Serilog.Sinks.InMemory;

//namespace My.Tests.Middleware;
//public class RequestLoggingMiddlewareTests
//{
//    [Fact]
//    public async Task Invoke_LogsExpectedProperties()
//    {
//        // Arrange
//        //var memoryLogger = new MemorySink();
//        Log.Logger = new LoggerConfiguration()
//            .Enrich.FromLogContext()
//            .WriteTo.InMemory()
//            .CreateLogger();
//        //new LoggerConfiguration()
//        //.WriteTo.Sink(memoryLogger)
//        //.CreateLogger();

//        var context = new DefaultHttpContext();
//        context.Request.Headers["X-Customer-Id"] = "12345";

//        var next = new Mock<RequestDelegate>();
//        var middleware = new RequestLoggingMiddleware(next.Object);

//        // Act
//        await middleware.Invoke(context).ConfigureAwait(true);
//        Log.Logger.Debug("Test");
//        // Assert
//        Assert.Contains(memoryLogger.Events, le => le.Properties.ContainsKey("X-Customer-Id"));
//        Assert.Contains(memoryLogger.Events, le => le.Properties["X-Customer-Id"].ToString().Contains("12345"));
//    }

//    class MemorySink : ILogEventSink
//    {
//        public List<LogEvent> Events { get; } = new List<LogEvent>();

//        public void Emit(LogEvent logEvent)
//        {
//            Events.Add(logEvent);
//        }
//    }
//}
