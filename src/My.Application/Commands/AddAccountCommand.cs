using MediatR;
using My.Domain.Models.Domain;

namespace My.AppHandlers.Commands;

public record AddAccountCommand(DomainAccount Account) : IRequest<DomainAccountResponse>;
