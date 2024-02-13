using MediatR;
using My.AppHandlers.Queries;
using My.Domain.Contracts;
using My.Domain.Enums;
using My.Domain.Models.Domain;
using My.Domain.Models.Legacy;
using AutoMapper;
using My.Domain.Models.Modern;

namespace My.AppHandlers.Handlers;

public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, DomainAccount>
{
    private readonly IRepositoryLegacy _legacyRepo;
    private readonly IRepositoryModern _modernRepo;
    private readonly ISysRouter _router;
    private readonly IMapper _mapper;

    public GetAccountByIdHandler(
        IRepositoryLegacy legacyRepo,
        IRepositoryModern modernRepo,
        ISysRouter router,
        IMapper mapper)
    {
        _legacyRepo = legacyRepo;
        _modernRepo = modernRepo;
        _router = router;
        _mapper = mapper;
    } 

    public async Task<DomainAccount> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        TargetSystem system = _router.GetRoute(request.Id);
        if (system == TargetSystem.Legacy)
        {
            LegacyAccount account = await _legacyRepo.GetAccountById(request.Id).ConfigureAwait(false);
            return _mapper.Map<DomainAccount>(account);
        }
        else
        {
            ModernAccount account = await _modernRepo.GetAccountById(request.Id).ConfigureAwait(false);
            return _mapper.Map<DomainAccount>(account);
        }
    }

}
