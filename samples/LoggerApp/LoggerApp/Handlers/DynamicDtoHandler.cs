using System.Text.Json;

namespace LoggerApp.Handlers;

public class DynamicDtoHandler
{
    public static async Task<IResult> HandleDynamicDtoAsync(HttpRequest request)
    {
        // Read and deserialize the request body to a dynamic object
        var json = await new StreamReader(request.Body).ReadToEndAsync();
        dynamic data = JsonSerializer.Deserialize<dynamic>(json);

        // Logic to handle the dynamic data
        // For example, logging, processing, or storing the data

        // Return a response
        return Results.Ok(new { message = "Received dynamic DTO", receivedData = data });
    }
}
