using MediatR;
using My.Domain.Enums;

namespace My.Application.UseCases.RouteGet;
public record GetRouteByIdQuery(int Id) : IRequest<TargetSystem>;
