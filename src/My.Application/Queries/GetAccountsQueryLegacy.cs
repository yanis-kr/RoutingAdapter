using MediatR;
using My.Domain.Models.Legacy;

namespace My.AppHandlers.Queries;

public record GetAccountsQueryLegacy() : IRequest<IEnumerable<LegacyAccount>>;
