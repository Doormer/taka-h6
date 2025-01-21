using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.UnreadMessageAggregate;

public class UnreadMessage : Entity, IAggregateRoot
{
    public UnreadMessage(Guid messageId, Guid contactId)
    {
        MessageID = messageId;
        ContactId = contactId;
    }

    public Guid MessageID { get; private set; }
    public Guid ContactId { get; private set; }
    public DateTime? ReadTime { get; private set; }

    //     public int NumOfUnreadMessages { get; private set; }

    //     public int GetUnreadMessageCount()
    //     {
    //         return NumOfUnreadMessages;
    //     }
}