using MediatR;
using My.Domain.Models.Modern;

namespace My.Application.UseCases.Account.Queries;

public record GetAccountsQueryModern() : IRequest<IEnumerable<ModernAccount>>;
