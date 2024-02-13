using MediatR;
using My.Domain.Models.Modern;

namespace My.AppHandlers.Queries;

public record GetAccountsQueryModern() : IRequest<IEnumerable<ModernAccount>>;
