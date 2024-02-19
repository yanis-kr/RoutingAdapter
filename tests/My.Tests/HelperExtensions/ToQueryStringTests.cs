using System.Text.Json.Serialization;
namespace My.Tests.HelperExtensions;

public class QueryStringExtensionsTests
{
    [Fact]
    public void ToQueryString_ReturnsCorrectQueryString()
    {
        // Arrange
        var queryParams = new TestQueryParams
        {
            Param1 = "TestValue",
            Param2 = 123,
            Param3 = true
        };

        // Act
        var queryString = queryParams.ToQueryString();

        // Assert
        var expected = "param1=TestValue&param2=123&param3=True";
        Assert.Equal(expected, queryString);
    }

    private class TestQueryParams
    {
        [JsonPropertyName("param1")]
        public string? Param1 { get; set; }

        [JsonPropertyName("param2")]
        public int Param2 { get; set; }

        [JsonPropertyName("param3")]
        public bool Param3 { get; set; }
    }
}
