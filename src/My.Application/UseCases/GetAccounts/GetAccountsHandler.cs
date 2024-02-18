using MediatR;
using My.Domain.Contracts;
using AutoMapper;
using My.Domain.Models.Domain;
using My.Domain.Enums;

namespace My.Application.UseCases.GetAccounts;

public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, IEnumerable<DomainAccount>>
{
    private readonly IRepositoryLegacy _legacyRepo;
    private readonly IRepositoryModern _modernRepo;
    private readonly IFeatureFlag _featureFlag;
    private readonly IMapper _mapper;

    public GetAccountsHandler(
        IRepositoryLegacy legacyRepo,
        IRepositoryModern modernRepo,
        IFeatureFlag featureFlag,
        IMapper mapper)
    {
        _legacyRepo = legacyRepo;
        _modernRepo = modernRepo;
        _featureFlag = featureFlag;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DomainAccount>> Handle(GetAccountsQuery request,
        CancellationToken cancellationToken)
    {
        if (_featureFlag.IsFeatureEnabled(FeatureFlag.FeatureDefaultSystemLegacy))
        {
            var accountsLegacy = await _legacyRepo.GetAllAccounts().ConfigureAwait(false);
            var accountsDomain = _mapper.Map<IEnumerable<DomainAccount>>(accountsLegacy);
            return accountsDomain;
        }
        else
        {
            var accountsModern = await _modernRepo.GetAllAccounts().ConfigureAwait(false);
            var accountsDomain = _mapper.Map<IEnumerable<DomainAccount>>(accountsModern);
            return accountsDomain;
        }
    }

}
