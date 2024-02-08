using Microsoft.Extensions.Logging;
using My.Domain.Contracts;
using My.Domain.Models.Domain;

namespace My.Infrastructure.EventBus;
public class EventBusStub : IEventBus
{
    private readonly ILogger<EventBusStub> _logger;

    public EventBusStub(ILogger<EventBusStub> logger)
    {
        _logger = logger;
    }
    public Task AccountEventOccured(DomainAccount account, string evt)
    {
        //log the event
        _logger.LogInformation($"Event {evt} occured for account {account.Id} - {account.Name}");
        return Task.CompletedTask;
    }
}
