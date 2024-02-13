using MediatR;
using My.Domain.Models.Modern;

namespace My.AppHandlers.Queries;

public record GetAccountsQuerySys2() : IRequest<IEnumerable<ModernAccount>>;
