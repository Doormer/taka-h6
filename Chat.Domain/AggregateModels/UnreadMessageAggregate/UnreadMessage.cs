using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.UnreadMessageAggregate;

public class UnreadMessage : Entity, IAggregateRoot
{
    public UnreadMessage(Guid userId, Guid contactUserId)
    {
        UserId = userId;
        ContactUserId = contactUserId;
    }
    public Guid UserId { get; private set; }
    public Guid ContactUserId { get; }
    public int NumOfUnreadMessages { get; private set; }
    public Contact? Contact { get; private set; }
    public int GetUnreadMessageCount()
    {
        return NumOfUnreadMessages;
    }

}