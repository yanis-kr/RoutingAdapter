using System.Text.Json;
using LoggerApp.Models;

namespace LoggerApp;
public class HttpBinClient
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpBinClient(HttpClient client, IHttpContextAccessor httpContextAccessor)
    {
        _client = client;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GetAnythingAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "anything");
        var correlationId = _httpContextAccessor.HttpContext?.Request.Headers["CorrelationId"].ToString();
        if (!string.IsNullOrEmpty(correlationId))
        {
            request.Headers.Add("CorrelationId", correlationId);
        }

        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<HttpbinResponseDto?> PostAnythingAsync(object dto)
    {
        var jsonContent = JsonSerializer.Serialize(dto);
        var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("anything", content);
        response.EnsureSuccessStatusCode();

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var responseStream = await response.Content.ReadAsStreamAsync();
        var responseDto = await JsonSerializer.DeserializeAsync<HttpbinResponseDto>(responseStream, options);

        return responseDto;
    }

}
