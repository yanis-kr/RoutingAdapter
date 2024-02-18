using MediatR;
using My.Domain.Contracts;
using AutoMapper;
using My.Domain.Models.Legacy;

namespace My.Application.UseCases.GetAccounts;

public class GetAccountsHandlerLegacy : IRequestHandler<GetAccountsQueryLegacy, IEnumerable<LegacyAccount>>
{
    private readonly IRepositoryLegacy _legacyRepo;
    private readonly IMapper _mapper;

    public GetAccountsHandlerLegacy(
        IRepositoryLegacy legacyRepo,
        IMapper mapper)
    {
        _legacyRepo = legacyRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LegacyAccount>> Handle(GetAccountsQueryLegacy request,
        CancellationToken cancellationToken)
    {
        var accountsLegacy = await _legacyRepo.GetAllAccounts().ConfigureAwait(false);
        //var accountsDomain = _mapper.Map<IEnumerable<LegacyAccount>>(accountsLegacy);
        return accountsLegacy;
    }

}
