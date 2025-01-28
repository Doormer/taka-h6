using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.UnreadMessageAggregate;
public class Message(Guid contactId) : Entity
{
    public Guid ContactId { get; private set; } = contactId;
}