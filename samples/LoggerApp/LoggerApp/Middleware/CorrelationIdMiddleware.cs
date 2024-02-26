using Serilog;

namespace LoggerApp.Middleware;
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Headers.TryGetValue(Constants.CorrelationIdName, out var correlationIdValues);
        var correlationId = correlationIdValues.FirstOrDefault();
        if (string.IsNullOrEmpty(correlationId))
        {
            correlationId = System.Guid.NewGuid().ToString();
            context.Request.Headers.Append(Constants.CorrelationIdName, correlationId);
            Log.Information($"Adding new X-Correlation-ID: {correlationId}");
        } else
        {
            Log.Information($"Found X-Correlation-ID: {correlationId}");
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }
}

