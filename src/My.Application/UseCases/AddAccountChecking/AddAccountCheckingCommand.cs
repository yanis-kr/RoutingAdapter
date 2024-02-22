using MediatR;
using My.Domain.Models.Domain;

namespace My.Application.UseCases.AddAccount;

public record AddAccountCheckingCommand(DomainAccount Account) : IRequest<DomainAccountResponse>;
