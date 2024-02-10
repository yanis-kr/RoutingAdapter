using MediatR;
using My.Domain.Models.MySys2;

namespace My.AppHandlers.Queries;

public record GetAccountsQuerySys2() : IRequest<IEnumerable<MySys2Account>>;
