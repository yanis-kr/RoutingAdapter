using AutoMapper;
using Moq;
using My.AppHandlers.Queries;
using My.Application.UseCases.Account.Handlers;
using My.Application.UseCases.Account.Profiles;
using My.Domain.Contracts;
using My.Domain.Enums;
using My.Domain.Models.Domain;
using My.Domain.Models.Legacy;
using My.Domain.Models.Modern;

namespace My.Tests.Handlers;
public class GetAccountByIdHandlerTests
{
    private readonly Mock<IRepositoryLegacy> _mockRepoSys1;
    private readonly Mock<IRepositoryModern> _mockRepoSys2;
    private readonly Mock<ISysRouter> _mockSysRouter;
    private readonly IMapper _mapper;
    private readonly GetAccountByIdHandler _handler;

    public GetAccountByIdHandlerTests()
    {
        _mockRepoSys1 = new Mock<IRepositoryLegacy>();
        _mockRepoSys1.Setup(repo => repo.GetAccountById(1))
            .ReturnsAsync(new LegacyAccount { Id = 1, Name = "Test Account1" });

        _mockRepoSys2 = new Mock<IRepositoryModern>();
        _mockRepoSys2.Setup(repo => repo.GetAccountById(2))
            .ReturnsAsync(new ModernAccount { Id = 2, Name = "Test Account2" });

        _mockSysRouter = new Mock<ISysRouter>();
        _mockSysRouter.Setup(router => router.GetRoute(1)).Returns(TargetSystem.Legacy);
        _mockSysRouter.Setup(router => router.GetRoute(2)).Returns(TargetSystem.Modern);

        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile<AccountProfile>());
        _mapper = configuration.CreateMapper();

        _handler = new GetAccountByIdHandler(_mockRepoSys1.Object, _mockRepoSys2.Object, _mockSysRouter.Object, _mapper); // Update this
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
