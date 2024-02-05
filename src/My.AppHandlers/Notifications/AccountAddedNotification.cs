using MediatR;
using My.Domain.Models.Domain;

namespace My.AppHandlers.Notifications;
public record AccountAddedNotification(DomainAccount Account) : INotification;
