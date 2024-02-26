using System.Text.Json;

namespace LoggerApp.Handlers;

public partial class DynamicDtoHandler
{
    public static async Task<IResult> HandleDynamicDtoAsync2(HttpRequest request)
    {
        // Read and deserialize the request body to a dynamic object
        var json = await new StreamReader(request.Body).ReadToEndAsync();
        dynamic data = JsonSerializer.Deserialize<dynamic>(json);

        // Logic to handle the dynamic data
        // For example, logging, processing, or storing the data

        // Return a response
        return Results.Ok(new { message = "Received dynamic DTO", receivedData = data });
    }

    public static async Task<IResult> HandleDynamicDtoAsync(HttpRequest request)
    {
        // Read and deserialize the request body to a dynamic object
        var json = await new StreamReader(request.Body).ReadToEndAsync();
        dynamic data = JsonSerializer.Deserialize<dynamic>(json);

        // Retrieve the CorrelationId from the request headers
        var correlationId = request.Headers[Constants.CorrelationIdName].ToString();

        // Logic to handle the dynamic data here

        // Prepare the response with a custom result to manipulate headers
        var response = Results.Ok(new { message = "Received dynamic DTO", receivedData = data });

        // Return a custom IResult that adds the CorrelationId header
        return new CustomResult(response, correlationId);
    }
}
