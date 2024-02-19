using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using My.Domain.Contracts;
using My.Domain.Enums;

namespace My.Infrastructure.MySysRouter;

public class MySysRouterHttpStub : ISysRouter
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MySysRouterHttpStub> _logger;

    public MySysRouterHttpStub(HttpClient httpClient,
        ILogger<MySysRouterHttpStub> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    }

    public async Task AddRoute(int accountId, TargetSystem target)
    {
        var post = new Post
        {
            Id = accountId,
            Title = "Route to " + target.ToString()
        };

        var json = JsonSerializer.Serialize(post);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _logger.LogInformation("Adding post (emulating route post): {Post}", post);

        var response = await _httpClient.PostAsync("posts/", content)
            .ConfigureAwait(true);

        if (!response.IsSuccessStatusCode)
        {
            // Handle non-success status codes appropriately
            throw new HttpRequestException($"Failed to add post/route. Status code: {response.StatusCode}");
        }

    }

    public async Task<TargetSystem> GetRoute(int accountId)
    {
        var response = await _httpClient.GetAsync($"posts/{accountId}").ConfigureAwait(true);

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new HttpRequestException("404 Not Found", null, System.Net.HttpStatusCode.NotFound);
            }
            response.EnsureSuccessStatusCode(); // This will throw for non-success codes
        }

        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
        var post = JsonSerializer.Deserialize<Post>(content); // Replaced JsonConvert with JsonSerializer
        _logger.LogInformation("Received post: {Post}", post);

        // Just for this example Legacy  system keeps accounts #1,3
        // and Modern system keeps accounts #2,3
        var LegacyRoutes = new int[] { 1, 3 };
        var targetSystem = LegacyRoutes.Contains(post?.Id ?? 0) ?
            TargetSystem.Legacy : TargetSystem.Modern;
        return targetSystem;
    }

    // Assuming a simple Post class that matches the JSON structure from the API
    private class Post
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}
