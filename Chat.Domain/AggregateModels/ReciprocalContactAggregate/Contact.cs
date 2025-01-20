using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ReciprocalContactAggregate;

public sealed class Contact(Guid userId, Guid contactUserId) : Entity
{
    public Guid UserId { get; } = userId;
    public Guid ContactUserId { get; } = contactUserId;

    public Contact CreateMirror()
    {
        return new Contact(ContactUserId, UserId);
    }
}