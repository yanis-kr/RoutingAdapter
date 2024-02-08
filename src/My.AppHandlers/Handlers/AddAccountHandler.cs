using MediatR;
using My.AppHandlers.Commands;
using My.Domain.Contracts;
using My.Domain.Models.Domain;
using AutoMapper;
using My.Domain.Enums;
using My.Domain.Models.MySys1;
using My.Domain.Models.MySys2;

namespace My.AppHandlers.Handlers;

public class AddAccountHandler : IRequestHandler<AddAccountCommand, DomainAccount>
{
    private readonly IRepositoryMySys1 _mySys1Repo;
    private readonly IRepositoryMySys2 _mySys2Repo;
    private readonly ISysRouter _router;
    private readonly IFeatureFlag _featureFlag;
    private readonly IMapper _mapper;

    public AddAccountHandler(
        IRepositoryMySys1 mySys1Repo,
        IRepositoryMySys2 mySys2Repo,
        ISysRouter router,
        IFeatureFlag featureFlag,
        IMapper mapper)
    {
        _mySys1Repo = mySys1Repo;
        _mySys2Repo = mySys2Repo;
        _router = router;
        _featureFlag = featureFlag;
        _mapper = mapper;
    }

    public async Task<DomainAccount> Handle(AddAccountCommand request, CancellationToken cancellationToken)
    {
        var isSys1 = _featureFlag.IsFeatureEnabled(FeatureFlag.FeatureDefaultSystemSys1);
        if (isSys1)
        {
            var account = _mapper.Map<DomainAccount, MySys1Account>(request.Account);
            await _mySys1Repo.AddAccount(account).ConfigureAwait(false);
        }
        else
        {
            var account = _mapper.Map<DomainAccount, MySys2Account>(request.Account);
            await _mySys2Repo.AddAccount(account).ConfigureAwait(false);
        }

        return request.Account;
    }
}
