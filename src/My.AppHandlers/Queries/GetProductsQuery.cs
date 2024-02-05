using MediatR;
using My.Domain.Models.Domain;

namespace My.AppHandlers.Queries;

public record GetAccountsQuery() : IRequest<IEnumerable<DomainAccount>>;
