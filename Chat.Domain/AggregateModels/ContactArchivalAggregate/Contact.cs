using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ContactArchivalAggregate;

/// <summary>
/// Represents a contact value object that contains archival status information.
/// Immutable by design as part of the Contact Archival aggregate.
/// </summary>
public class Contact : ValueObject
{
    public int Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ContactUserId { get; private set; }
    public bool IsArchived { get; private set; }

    public Contact(Guid userId, Guid contactUserId, bool isArchived)
    {
        UserId = userId;
        ContactUserId = contactUserId;
        IsArchived = isArchived;
    }

    internal void UpdateArchivedStatus(bool isArchived)
    {
        IsArchived = isArchived;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return UserId;
        yield return ContactUserId;
        yield return IsArchived;
    }
}