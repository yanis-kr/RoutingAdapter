using MediatR;
using My.Domain.Models.Domain;

namespace My.Application.UseCases.GetAccounts;

public record GetAccountsQuery() : IRequest<IEnumerable<DomainAccount>>;
