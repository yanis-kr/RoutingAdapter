using MediatR;
using My.Domain.Models.Domain;

namespace My.Application.UseCases.Account.Commands;

public record AddAccountCommand(DomainAccount Account) : IRequest<DomainAccountResponse>;
