using LoggerApp.Handlers;
using LoggerApp.Models;

namespace LoggerApp;

public static class ApiEndpoints
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        // Delegate with a method body for potentially complex logic.
        app.MapGet("/api/echo", async (HttpBinClient client) =>
        {
            // Sends GET request to external API https://httpbin.org/ and returns it's echo response
            return await client.GetAnythingAsync();
        });

        // Delegate as a single expression for simple operations.
        app.MapPost("/api/echo", async (MyDto dto, HttpBinClient client) =>
            await MyDtoHandler.HandlePostAsync(dto, client)); // Directly returns a method call.

        app.MapPost("/api/dynamic", async (HttpRequest request) =>
        {
            // Delegate the request handling to the DynamicDtoHandler class
            return await DynamicDtoHandler.HandleDynamicDtoAsync(request);
        });

    }
}
