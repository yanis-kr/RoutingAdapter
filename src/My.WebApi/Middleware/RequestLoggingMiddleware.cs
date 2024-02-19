using Microsoft.Extensions.Options;
using My.Domain.ConfigOptions;
using Serilog.Context;

namespace My.WebApi.Middleware;
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptions<CustomLogging> _optionsCustomLogging;

    public RequestLoggingMiddleware(RequestDelegate next,
        IOptions<CustomLogging> optionsCustomLogging)
    {
        _next = next;
        _optionsCustomLogging = optionsCustomLogging;
    }

    public async Task Invoke(HttpContext context)
    {
        // Use a helper method to add properties to the LogContext
        foreach (var header in _optionsCustomLogging.Value.HeadersToLog)
        {
            AddPropertyToLogContext(context, header);
        }
        //AddPropertyToLogContext(context, "X-Customer-Id");
        //AddPropertyToLogContext(context, "X-Session-Id");
        //AddPropertyToLogContext(context, "X-Correlation-Id");

        foreach (var header in context.Request.Headers)
        {
            System.Diagnostics.Debug.WriteLine($"{header.Key}: {header.Value}");
        }

        await _next(context).ConfigureAwait(true);
    }

    private static void AddPropertyToLogContext(HttpContext context, string propertyName)
    {
        if (context.Request.Headers.TryGetValue(propertyName, out var propertyValue))
        {
            LogContext.PushProperty(propertyName, propertyValue);
        }
    }
}
