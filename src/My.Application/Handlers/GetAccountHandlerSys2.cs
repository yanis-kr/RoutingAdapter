using MediatR;
using My.AppHandlers.Queries;
using My.Domain.Contracts;
using AutoMapper;
using My.Domain.Models.Modern;

namespace My.AppHandlers.Handlers;

public class GetAccountsHandlerSys2 : IRequestHandler<GetAccountsQuerySys2, IEnumerable<ModernAccount>>
{
    private readonly IRepositoryModern _modernRepo;
    private readonly IMapper _mapper;

    public GetAccountsHandlerSys2(
        IRepositoryModern modernRepo,
        IMapper mapper)
    {
        _modernRepo = modernRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ModernAccount>> Handle(GetAccountsQuerySys2 request,
        CancellationToken cancellationToken)
    {
        var accountsSys2 = await _modernRepo.GetAllAccounts().ConfigureAwait(false);
        //var accountsDomain = _mapper.Map<IEnumerable<DomainAccount>>(accountsSys2);
        return accountsSys2;
    }

}
