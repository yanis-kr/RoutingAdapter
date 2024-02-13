using AutoMapper;
using Moq;
using My.AppCore.Profiles;
using My.AppHandlers.Handlers;
using My.AppHandlers.Queries;
using My.Domain.Contracts;
using My.Domain.Enums;
using My.Domain.Models.Domain;
using My.Domain.Models.Legacy;
using My.Domain.Models.Modern;

namespace My.Tests.Handlers;
public class GetAccountByIdHandlerTests
{
    private readonly Mock<IRepositoryLegacy> _mockRepoLegacy;
    private readonly Mock<IRepositoryModern> _mockRepoModern;
    private readonly Mock<ISysRouter> _mockSysRouter;
    private readonly IMapper _mapper;
    private readonly GetAccountByIdHandler _handler;

    public GetAccountByIdHandlerTests()
    {
        _mockRepoLegacy = new Mock<IRepositoryLegacy>();
        _mockRepoLegacy.Setup(repo => repo.GetAccountById(1))
            .ReturnsAsync(new LegacyAccount { Id = 1, Name = "Test Account1" });

        _mockRepoModern = new Mock<IRepositoryModern>();
        _mockRepoModern.Setup(repo => repo.GetAccountById(2))
            .ReturnsAsync(new ModernAccount { Id = 2, Name = "Test Account2" });

        _mockSysRouter = new Mock<ISysRouter>();
        _mockSysRouter.Setup(router => router.GetRoute(1)).Returns(TargetSystem.Legacy);
        _mockSysRouter.Setup(router => router.GetRoute(2)).Returns(TargetSystem.Modern);

        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile<AccountProfile>());
        _mapper = configuration.CreateMapper();

        _handler = new GetAccountByIdHandler(_mockRepoLegacy.Object, _mockRepoModern.Object, _mockSysRouter.Object, _mapper); // Update this
    }

    [Fact]
    public async Task Handle_GivenValidId_ReturnsAccount()
    {
        // Arrange
        var accountId = 1;
        var query = new GetAccountByIdQuery(accountId);
        var cancellationToken = CancellationToken.None;

        // Act
        DomainAccount result = await _handler.Handle(query, cancellationToken).ConfigureAwait(false);

        Assert.NotNull(result);
        Assert.Equal(accountId, result.Id);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ThrowsError()
    {
        // Arrange
        var accountId = 4;
        var query = new GetAccountByIdQuery(accountId);
        var cancellationToken = CancellationToken.None;

        // Act
        DomainAccount result = await _handler.Handle(query, cancellationToken).ConfigureAwait(false);

        Assert.Null(result);
    }
}
