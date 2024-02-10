using MediatR;
using My.AppHandlers.Queries;
using My.Domain.Contracts;
using AutoMapper;
using My.Domain.Models.MySys1;

namespace My.AppHandlers.Handlers;

public class GetAccountsHandlerSys1 : IRequestHandler<GetAccountsQuerySys1, IEnumerable<MySys1Account>>
{
    private readonly IRepositoryMySys1 _mySys1Repo;
    private readonly IMapper _mapper;

    public GetAccountsHandlerSys1(
        IRepositoryMySys1 mySys1Repo,
        IMapper mapper)
    {
        _mySys1Repo = mySys1Repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MySys1Account>> Handle(GetAccountsQuerySys1 request,
        CancellationToken cancellationToken)
    {
        var accountsSys1 = await _mySys1Repo.GetAllAccounts().ConfigureAwait(false);
        //var accountsDomain = _mapper.Map<IEnumerable<MySys1Account>>(accountsSys1);
        return accountsSys1;
    }

}
