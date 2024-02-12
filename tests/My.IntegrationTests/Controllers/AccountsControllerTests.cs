using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace My.IntegrationTests.Controllers;
public class AccountsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AccountsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAccountById_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/accounts/1").ConfigureAwait(true);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
