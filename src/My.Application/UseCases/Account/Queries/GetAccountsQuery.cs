using MediatR;
using My.Domain.Models.Domain;

namespace My.Application.UseCases.Account.Queries;

public record GetAccountsQuery() : IRequest<IEnumerable<DomainAccount>>;
