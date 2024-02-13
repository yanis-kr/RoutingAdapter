using MediatR;
using My.Domain.Contracts;
using AutoMapper;
using My.Domain.Models.Legacy;
using My.Application.UseCases.Account.Queries;

namespace My.Application.UseCases.Account.Handlers;

public class GetAccountsHandlerSys1 : IRequestHandler<GetAccountsQuerySys1, IEnumerable<LegacyAccount>>
{
    private readonly IRepositoryLegacy _legacyRepo;
    private readonly IMapper _mapper;

    public GetAccountsHandlerSys1(
        IRepositoryLegacy legacyRepo,
        IMapper mapper)
    {
        _legacyRepo = legacyRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LegacyAccount>> Handle(GetAccountsQuerySys1 request,
        CancellationToken cancellationToken)
    {
        var accountsSys1 = await _legacyRepo.GetAllAccounts().ConfigureAwait(false);
        //var accountsDomain = _mapper.Map<IEnumerable<LegacyAccount>>(accountsSys1);
        return accountsSys1;
    }

}
