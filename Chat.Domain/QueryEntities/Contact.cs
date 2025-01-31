using Chat.Domain.SeedWork;

namespace Chat.Domain.QueryEntities;

public sealed class Contact : Entity
{
    public Contact(Guid userId, Guid contactUserId, bool isArchived)
    {
        UserId = userId;
        ContactUserId = contactUserId;
        IsArchived = isArchived;
        lastMessage = "我是全公司最帅的男人！";
    }

    public Guid UserId { get; private set; }
    public Guid ContactUserId { get; private set; }
    public bool IsArchived { get; private set; }
    public string lastMessage { get; private set; }
}