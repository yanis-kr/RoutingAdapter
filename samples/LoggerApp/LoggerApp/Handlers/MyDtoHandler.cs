using LoggerApp.Models;

namespace LoggerApp.Handlers;

public static class MyDtoHandler
{
    public static async Task<IResult> HandlePostAsync(MyDto dto, HttpBinClient client)
    {
        // Forward the DTO to https://httpbin.org/anything using the HttpBinClient
        var httpBinResponseDto = await client.PostAnythingAsync(dto);

        // Return the response from httpbin.org/anything
        return Results.Ok(new
        {
            Message = "DTO forwarded to httpbin.org/anything",
            HttpBinResponse = httpBinResponseDto // Now directly passing the deserialized object
        });
    }
}
