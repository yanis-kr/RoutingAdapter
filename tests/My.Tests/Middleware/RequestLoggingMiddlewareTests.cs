using Microsoft.AspNetCore.Http;
using Moq;
using My.WebApi.Middleware;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace My.Tests.Middleware;
public class RequestLoggingMiddlewareTests
{
    [Fact]
    public async Task Invoke_LogsExpectedProperties()
    {
        // Arrange
        var memoryLogger = new MemorySink();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Sink(memoryLogger)
            .CreateLogger();

        var context = new DefaultHttpContext();
        context.Request.Headers["X-CustomerId"] = "12345";
        context.Request.Headers["X-SessionId"] = "abcde";

        var next = new Mock<RequestDelegate>();
        var middleware = new RequestLoggingMiddleware(next.Object);

        // Act
        await middleware.Invoke(context).ConfigureAwait(true);

        // Assert
        Assert.Contains(memoryLogger.Events, logEvent =>
            logEvent.Properties.ContainsKey("CustomerId") &&
            logEvent.Properties["CustomerId"].ToString().Contains("12345") &&
            logEvent.Properties.ContainsKey("SessionId") &&
            logEvent.Properties["SessionId"].ToString().Contains("abcde"));
    }

    class MemorySink : ILogEventSink
    {
        public List<LogEvent> Events { get; } = new List<LogEvent>();

        public void Emit(LogEvent logEvent)
        {
            Events.Add(logEvent);
        }
    }
}
