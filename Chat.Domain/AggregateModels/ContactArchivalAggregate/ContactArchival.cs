using Chat.Domain.SeedWork;
using MediatR;

namespace Chat.Domain.AggregateModels.ContactArchivalAggregate;

public class ContactArchival : Entity, IAggregateRoot
{
    public Guid UserId { get; private set; }
    public Contact? Contact { get; private set; }

    private ContactArchival() { } // For EF Core

    public ContactArchival(Guid userId)
    {
        UserId = userId;
    }

    public void ArchiveContact(Guid contactUserId)
    {
        if (Contact is null)
        {
            Contact = new Contact(UserId, contactUserId, true);
        }
        else
        {
            Contact.UpdateArchivedStatus(true);
        }
    }

    public void UnarchiveContact(Guid contactUserId)
    {
        if (Contact?.ContactUserId != contactUserId)
        {
            throw new DomainException($"Contact with ID {contactUserId} not found");
        }

        Contact.UpdateArchivedStatus(false);
    }
}