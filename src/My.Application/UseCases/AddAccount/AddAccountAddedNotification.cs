using MediatR;
using My.Domain.Models.Domain;

namespace My.Application.UseCases.AddAccount;
public record AddAccountAddedNotification(DomainAccount Account) : INotification;
