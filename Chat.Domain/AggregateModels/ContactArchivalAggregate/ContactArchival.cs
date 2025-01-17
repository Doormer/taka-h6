using Chat.Domain.SeedWork;
using MediatR;

namespace Chat.Domain.AggregateModels.ContactArchivalAggregate;

/// <summary>
/// Represents the aggregate root for contact archival management.
/// Handles the archival status of contacts for a specific user.
/// </summary>
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