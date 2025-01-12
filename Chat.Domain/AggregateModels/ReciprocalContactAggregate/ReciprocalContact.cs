using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ReciprocalContactAggregate;

public class ReciprocalContact : Entity, IAggregateRoot
{
    public Guid UserId { get; private set; }
    public Guid UserContactId { get; private set; }
    private Contact _contact;

    public ReciprocalContact(Guid userId, Guid contactId)
    {
        UserId = userId;
        UserContactId = contactId;
        _contact = new Contact(userId, contactId);
    }

    public (Contact, Contact) GetReciprocalContact()
    {
        return (_contact, _contact.CreateMirror());
    }
}