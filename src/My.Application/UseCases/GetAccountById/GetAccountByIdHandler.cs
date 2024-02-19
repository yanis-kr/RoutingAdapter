using MediatR;
using My.Domain.Contracts;
using My.Domain.Enums;
using My.Domain.Models.Domain;
using AutoMapper;

namespace My.Application.UseCases.GetAccountById;

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
        var system = await _router.GetRoute(request.Id).ConfigureAwait(true);
        if (system == TargetSystem.Legacy)
        {
            var account = await _legacyRepo.GetAccountById(request.Id).ConfigureAwait(false);
            return _mapper.Map<DomainAccount>(account);
        }
        else
        {
            var account = await _modernRepo.GetAccountById(request.Id).ConfigureAwait(false);
            return _mapper.Map<DomainAccount>(account);
        }
    }

}
