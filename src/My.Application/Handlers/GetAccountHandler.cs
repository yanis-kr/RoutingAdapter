using MediatR;
using My.AppHandlers.Queries;
using My.Domain.Contracts;
using AutoMapper;
using My.Domain.Models.Domain;
using My.Domain.Enums;

namespace My.AppHandlers.Handlers;

public class GetAccountHandler : IRequestHandler<GetAccountsQuery, IEnumerable<DomainAccount>>
{
    private readonly IRepositoryMySys1 _mySys1Repo;
    private readonly IRepositoryMySys2 _mySys2Repo;
    private readonly IFeatureFlag _featureFlag;
    private readonly IMapper _mapper;

    public GetAccountHandler(
        IRepositoryMySys1 mySys1Repo,
        IRepositoryMySys2 mySys2Repo,
        IFeatureFlag featureFlag,
        IMapper mapper)
    {
        _mySys1Repo = mySys1Repo;
        _mySys2Repo = mySys2Repo;
        _featureFlag = featureFlag;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DomainAccount>> Handle(GetAccountsQuery request,
        CancellationToken cancellationToken)
    {
        if(_featureFlag.IsFeatureEnabled(FeatureFlag.FeatureDefaultSystemSys1))
        {
            var accountsSys1 = await _mySys1Repo.GetAllAccounts().ConfigureAwait(false);
            var accountsDomain = _mapper.Map<IEnumerable<DomainAccount>>(accountsSys1);
            return accountsDomain;
        } else
        {
            var accountsSys2 = await _mySys2Repo.GetAllAccounts().ConfigureAwait(false);
            var accountsDomain = _mapper.Map<IEnumerable<DomainAccount>>(accountsSys2);
            return accountsDomain;
        }
    }

}
