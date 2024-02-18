using Serilog.Context;

namespace My.WebApi.Middleware;
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Use a helper method to add properties to the LogContext
        AddPropertyToLogContext(context, "X-Customer-Id");
        AddPropertyToLogContext(context, "X-Session-Id");
        AddPropertyToLogContext(context, "X-Correlation-Id");
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
