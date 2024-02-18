using MediatR;
using My.Domain.Models.Modern;

namespace My.Application.UseCases.GetAccounts;

public record GetAccountsQueryModern() : IRequest<IEnumerable<ModernAccount>>;
