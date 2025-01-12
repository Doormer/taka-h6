using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ContactArchivalAggregate;

public class ContactArchival : Entity, IAggregateRoot
{
    public ContactArchival(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; private set; }

    public Contact? Contact { get; private set; }

    public void UpdateArchivedStatus(bool isArchived)
    {
        if (Contact is null)
        {
            throw new Exception("Contact is null");
        }

        Contact.UpdateArchivedStatus(isArchived);
    }
}