using My.Domain.Models.Domain;

namespace My.Domain.Contracts;
public interface IEventBus
{
    Task AccountEventOccured(DomainAccount account, string evt);
}
