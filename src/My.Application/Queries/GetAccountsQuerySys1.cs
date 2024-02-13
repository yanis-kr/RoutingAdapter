using MediatR;
using My.Domain.Models.Legacy;

namespace My.AppHandlers.Queries;

public record GetAccountsQuerySys1() : IRequest<IEnumerable<LegacyAccount>>;
