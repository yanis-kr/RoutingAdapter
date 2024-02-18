using MediatR;
using My.Domain.Models.Domain;

namespace My.Application.UseCases.AddAccount;

public record AddAccountCommand(DomainAccount Account) : IRequest<DomainAccountResponse>;
