using MediatR;
using My.AppHandlers.Queries;
using My.Domain.Contracts;
using AutoMapper;
using My.Domain.Models.MySys2;

namespace My.AppHandlers.Handlers;

public class GetAccountsHandlerSys2 : IRequestHandler<GetAccountsQuerySys2, IEnumerable<MySys2Account>>
{
    private readonly IRepositoryMySys2 _mySys2Repo;
    private readonly IMapper _mapper;

    public GetAccountsHandlerSys2(
        IRepositoryMySys2 mySys2Repo,
        IMapper mapper)
    {
        _mySys2Repo = mySys2Repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MySys2Account>> Handle(GetAccountsQuerySys2 request,
        CancellationToken cancellationToken)
    {
        var accountsSys2 = await _mySys2Repo.GetAllAccounts().ConfigureAwait(false);
        //var accountsDomain = _mapper.Map<IEnumerable<DomainAccount>>(accountsSys2);
        return accountsSys2;
    }

}
