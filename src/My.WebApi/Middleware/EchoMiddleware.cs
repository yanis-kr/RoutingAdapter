using Serilog;

namespace My.WebApi.Middleware;
public class EchoMiddleware
{
    private readonly RequestDelegate _next;

    public EchoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Bypass the middleware unless it is /echo
        if (!context.Request.Path.StartsWithSegments("/echo"))
        {
            await _next(context); 
            return;
        }

        // Copy request headers to response
        foreach (var header in context.Request.Headers)
        {
            Log.Information($"{header.Key}: {header.Value}");
            context.Response.Headers[header.Key] = header.Value;
        }

        // Echo the request body to the response
        if (context.Request.ContentLength > 0 && context.Request.Body.CanRead)
        {
            context.Request.EnableBuffering(); // Enable reading the body multiple times

            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0; // Reset the stream position for further reading

            // Write the body to the response
            await context.Response.WriteAsync(body);
        }

        // Call the next delegate/middleware in the pipeline
        await _next(context);
    }
}
