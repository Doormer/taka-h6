using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.UnreadMessageAggregate;

public class Contact(Guid userId, Guid contactUserId) : Entity
{
    public Guid UserId { get; private set; } = userId;
    public Guid ContactUserId { get; } = contactUserId;
}