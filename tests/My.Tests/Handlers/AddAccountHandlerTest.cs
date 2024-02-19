using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using My.Application.Exceptions;
using My.Application.UseCases.AddAccount;
using My.Domain.Contracts;
using My.Domain.Models.Domain;
using Xunit.Abstractions;

namespace My.Tests.Handlers;

public class AddAccountHandlerTests
{
    private readonly Mock<IRepositoryLegacy> _legacyRepoMock;
    private readonly Mock<IRepositoryModern> _modernRepoMock;
    private readonly Mock<ISysRouter> _routerMock;
    private readonly Mock<IFeatureFlag> _featureFlagMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ILogger<AddAccountHandler> _logger;
    private readonly ITestOutputHelper _output;

    public AddAccountHandlerTests(ITestOutputHelper output)
    {
        _logger = new Mock<ILogger<AddAccountHandler>>().Object;
        _legacyRepoMock = new Mock<IRepositoryLegacy>();
        _modernRepoMock = new Mock<IRepositoryModern>();
        _routerMock = new Mock<ISysRouter>();
        _featureFlagMock = new Mock<IFeatureFlag>();
        _mapperMock = new Mock<IMapper>();
        _output = output;
    }

    [Theory]
    [InlineData(1, "Test Account 1")]
    [InlineData(2, "Test Account 2")]
    public async Task Handle_ValidRequest_ReturnsDomainAccountResponse(int id, string name)
    {
        // Arrange
        var account = new DomainAccount
        {
            // Initialize fields here
            Name = name,
            Id = id
        };
        var request = new AddAccountCommand(account);
        var cancellationToken = CancellationToken.None;

        var handler = new AddAccountHandler(
            _logger,
            _legacyRepoMock.Object,
            _modernRepoMock.Object,
            _routerMock.Object,
            _featureFlagMock.Object,
            _mapperMock.Object);

        // Act
        var result = await handler.Handle(request, cancellationToken).ConfigureAwait(true);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainAccountResponse>(result);
        _output.WriteLine("AddAccountCommand received: {0}", request.Account.Name);
    }

    [Fact]
    public async Task Handle_InvalidRequest_RaisesValidationErrors()
    {
        // Arrange empty account
        var account = new DomainAccount();
        var request = new AddAccountCommand(account);
        var cancellationToken = CancellationToken.None;

        var handler = new AddAccountHandler(
            _logger,
            _legacyRepoMock.Object,
            _modernRepoMock.Object,
            _routerMock.Object,
            _featureFlagMock.Object,
            _mapperMock.Object);

        // Act and assert that the handler throws a ValidationException
        await Assert.ThrowsAsync<MyValidationException>(() => handler.Handle(request, cancellationToken)).ConfigureAwait(true);
        // var result = await handler.Handle(request, cancellationToken).ConfigureAwait(true);

        // // Assert
        // Assert.NotNull(result);
        // Assert.IsType<DomainAccountResponse>(result);
    }

}
