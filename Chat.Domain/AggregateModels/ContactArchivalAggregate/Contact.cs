using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ContactArchivalAggregate;

public class Contact : ValueObject
{
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