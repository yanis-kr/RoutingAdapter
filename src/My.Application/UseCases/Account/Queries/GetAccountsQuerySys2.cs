using MediatR;
using My.Domain.Models.Modern;

namespace My.Application.UseCases.Account.Queries;

public record GetAccountsQuerySys2() : IRequest<IEnumerable<ModernAccount>>;
