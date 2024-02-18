using MediatR;
using My.Domain.Models.Legacy;

namespace My.Application.UseCases.GetAccounts;

public record GetAccountsQueryLegacy() : IRequest<IEnumerable<LegacyAccount>>;
