using AutoMapper;
using Moq;
using My.AppHandlers.Commands;
using My.AppHandlers.Exceptions;
using My.AppHandlers.Handlers;
using My.Domain.Contracts;
using My.Domain.Models.Domain;

namespace My.Tests.Handlers;

public class AddAccountHandlerTests
{
    private readonly Mock<IRepositoryLegacy> _legacyRepoMock;
    private readonly Mock<IRepositoryModern> _modernRepoMock;
    private readonly Mock<ISysRouter> _routerMock;
    private readonly Mock<IFeatureFlag> _featureFlagMock;
    private readonly Mock<IMapper> _mapperMock;

    public AddAccountHandlerTests()
    {
        _legacyRepoMock = new Mock<IRepositoryLegacy>();
        _modernRepoMock = new Mock<IRepositoryModern>();
        _routerMock = new Mock<ISysRouter>();
        _featureFlagMock = new Mock<IFeatureFlag>();
        _mapperMock = new Mock<IMapper>();
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
    }

    [Fact]
    public async Task Handle_InvalidRequest_RaisesValidationErrors()
    {
        // Arrange empty account
        var account = new DomainAccount();
        var request = new AddAccountCommand(account);
        var cancellationToken = CancellationToken.None;

        var handler = new AddAccountHandler(
            _legacyRepoMock.Object,
            _modernRepoMock.Object,
            _routerMock.Object,
            _featureFlagMock.Object,
            _mapperMock.Object);

        // Act and assert that the handler throws a ValidationException
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(request, cancellationToken)).ConfigureAwait(true);
        // var result = await handler.Handle(request, cancellationToken).ConfigureAwait(true);

        // // Assert
        // Assert.NotNull(result);
        // Assert.IsType<DomainAccountResponse>(result);
    }

}
