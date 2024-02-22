using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using My.Application.UseCases.AddAccountChecking.RuleHandlers;
using My.Application.UseCases.AddAccountChecking.Rules;
using My.Domain.Contracts;
using My.Domain.Models.Domain;
using NRules;
using NRules.Fluent;

namespace My.Application.UseCases.AddAccount;

public class AddAccountCheckingHandler : IRequestHandler<AddAccountCheckingCommand, DomainAccountResponse>
{
    private readonly ILogger<AddAccountCheckingHandler> _logger;
    private readonly IRepositoryLegacy _legacyRepo;
    private readonly IRepositoryModern _modernRepo;
    private readonly ISysRouter _router;
    private readonly IFeatureFlag _featureFlag;
    private readonly IMapper _mapper;

    public AddAccountCheckingHandler(
        ILogger<AddAccountCheckingHandler> logger,
        IRepositoryLegacy legacyRepo,
        IRepositoryModern modernRepo,
        ISysRouter router,
        IFeatureFlag featureFlag,
        IMapper mapper)
    {
        _logger = logger;
        _legacyRepo = legacyRepo;
        _modernRepo = modernRepo;
        _router = router;
        _featureFlag = featureFlag;
        _mapper = mapper;
    }

    public async Task<DomainAccountResponse> Handle(AddAccountCheckingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AddAccountCommand received: {Account}", request.Account);

        var repository = new RuleRepository();
        repository.Load(x => x.From(typeof(Rule1).Assembly));

        var sessionFactory = repository.Compile(cancellationToken);
        var session = sessionFactory.CreateSession();

        var myEntity = new { SomeProperty = "1", SomeOtherProperty ="2"};
        session.Insert(myEntity);

        session.Fire(cancellationToken);

        var results = session.Query<RuleResult>().ToList();

        var responseMessage = "No rules ran";
        if (results.Any())
        {
            responseMessage = string.Join(", ", results.Select(r => r.Message));
        }

        var createAccountCommandResponse = new DomainAccountResponse();
        return await Task.FromResult(createAccountCommandResponse).ConfigureAwait(true);

    }
}
