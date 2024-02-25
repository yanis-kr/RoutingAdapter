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
        const string correlationIdName = "CorrelationId";
        context.Request.Headers.TryGetValue(correlationIdName, out var correlationIdValues);
        var correlationId = correlationIdValues.FirstOrDefault();
        if (string.IsNullOrEmpty(correlationId))
        {
            correlationId = System.Guid.NewGuid().ToString();
            context.Request.Headers.Append(correlationIdName, correlationId);
            Log.Information($"Adding new CorrelationId: {correlationId}");
        } else
        {
            Log.Information($"Found CorrelationId: {correlationId}");
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }
}

