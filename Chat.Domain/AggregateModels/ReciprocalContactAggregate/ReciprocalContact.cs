using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ReciprocalContactAggregate;

/// <summary>
/// Aggregate root for managing bidirectional contact relationships between users.
/// Ensures contact relationships are always reciprocal.
/// </summary>
public class ReciprocalContact(Guid userId, Guid contactUserId) : Entity, IAggregateRoot
{
    private Contact _contact  = new(userId, contactUserId);

    public (Contact, Contact) GetReciprocalContact()
    {
        return (_contact, _contact.CreateMirror());
    }
}