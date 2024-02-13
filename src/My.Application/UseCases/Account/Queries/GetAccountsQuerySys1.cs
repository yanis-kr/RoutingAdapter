using MediatR;
using My.Domain.Models.Legacy;

namespace My.Application.UseCases.Account.Queries;

public record GetAccountsQuerySys1() : IRequest<IEnumerable<LegacyAccount>>;
