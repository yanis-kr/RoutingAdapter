using MediatR;
using My.Domain.Models.Domain;

namespace My.Application.UseCases.Account.Notifications;
public record AccountAddedNotification(DomainAccount Account) : INotification;
