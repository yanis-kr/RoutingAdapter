using MediatR;
using My.Domain.Models.MySys1;

namespace My.AppHandlers.Queries;

public record GetAccountsQuerySys1() : IRequest<IEnumerable<MySys1Account>>;
