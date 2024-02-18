using MediatR;
using My.Domain.Contracts;
using AutoMapper;
using My.Domain.Models.Modern;

namespace My.Application.UseCases.GetAccounts;

public class GetAccountsHandlerModern : IRequestHandler<GetAccountsQueryModern, IEnumerable<ModernAccount>>
{
    private readonly IRepositoryModern _modernRepo;
    private readonly IMapper _mapper;

    public GetAccountsHandlerModern(
        IRepositoryModern modernRepo,
        IMapper mapper)
    {
        _modernRepo = modernRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ModernAccount>> Handle(GetAccountsQueryModern request,
        CancellationToken cancellationToken)
    {
        var accountsModern = await _modernRepo.GetAllAccounts().ConfigureAwait(false);
        //var accountsDomain = _mapper.Map<IEnumerable<DomainAccount>>(accountsModern);
        return accountsModern;
    }

}
