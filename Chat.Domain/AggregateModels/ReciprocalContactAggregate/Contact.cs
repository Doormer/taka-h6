using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ReciprocalContactAggregate;

/// <summary>
/// Represents a one-way contact relationship between users.
/// Part of the reciprocal contact pattern for managing user connections.
/// </summary>
public class Contact(Guid userId, Guid contactUserId) : Entity
{
    public Guid UserId { get; } = userId;
    public Guid ContactUserId { get; } = contactUserId;

    public Contact CreateMirror()
    {
        return new Contact(ContactUserId, UserId);
    }
}