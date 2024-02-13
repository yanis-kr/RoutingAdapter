using MediatR;
using My.Domain.Models.Legacy;

namespace My.Application.UseCases.Account.Queries;

public record GetAccountsQueryLegacy() : IRequest<IEnumerable<LegacyAccount>>;
