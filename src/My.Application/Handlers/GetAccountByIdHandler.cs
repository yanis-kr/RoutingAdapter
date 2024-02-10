using MediatR;
using My.AppHandlers.Queries;
using My.Domain.Contracts;
using My.Domain.Enums;
using My.Domain.Models.Domain;
using My.Domain.Models.MySys1;
using AutoMapper;
using My.Domain.Models.MySys2;

namespace My.AppHandlers.Handlers;

public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, DomainAccount>
{
    private readonly IRepositoryMySys1 _mySys1Repo;
    private readonly IRepositoryMySys2 _mySys2Repo;
    private readonly ISysRouter _router;
    private readonly IMapper _mapper;

    public GetAccountByIdHandler(
        IRepositoryMySys1 mySys1Repo,
        IRepositoryMySys2 mySys2Repo,
        ISysRouter router,
        IMapper mapper)
    {
        _mySys1Repo = mySys1Repo;
        _mySys2Repo = mySys2Repo;
        _router = router;
        _mapper = mapper;
    } 

    public async Task<DomainAccount> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        TargetSystem system = _router.GetRoute(request.Id);
        if (system == TargetSystem.MySys1)
        {
            MySys1Account account = await _mySys1Repo.GetAccountById(request.Id).ConfigureAwait(false);
            return _mapper.Map<DomainAccount>(account);
        }
        else
        {
            MySys2Account account = await _mySys2Repo.GetAccountById(request.Id).ConfigureAwait(false);
            return _mapper.Map<DomainAccount>(account);
        }
    }

}
