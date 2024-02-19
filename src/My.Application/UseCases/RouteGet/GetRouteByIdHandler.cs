using MediatR;
using My.Domain.Contracts;
using My.Domain.Enums;

namespace My.Application.UseCases.RouteGet;

public class GetRouteByIdHandler : IRequestHandler<GetRouteByIdQuery, TargetSystem>
{
    private readonly ISysRouter _router;

    public GetRouteByIdHandler(ISysRouter router)
    {
        _router = router;
    }

    public async Task<TargetSystem> Handle(GetRouteByIdQuery request, CancellationToken cancellationToken)
    {
        var system = await _router.GetRoute(request.Id).ConfigureAwait(true);
        return system;
    }

}
