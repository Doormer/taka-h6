using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.UnreadMessageAggregate;

public class UnreadMessage : Entity, IAggregateRoot
{
    public UnreadMessage(int contactId)
    {
        ContactId = contactId;
    }

    public int ContactId { get; private set; }
    public DateTime? ReadTime { get; private set; }

    //     public int NumOfUnreadMessages { get; private set; }

    //     public int GetUnreadMessageCount()
    //     {
    //         return NumOfUnreadMessages;
    //     }
}