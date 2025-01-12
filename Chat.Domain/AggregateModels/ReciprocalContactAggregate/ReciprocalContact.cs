using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ReciprocalContactAggregate;

public class ReciprocalContact(Guid userId, Guid contactUserId) : Entity, IAggregateRoot
{
    private readonly Contact _contact = new(userId, contactUserId);

    public (Contact, Contact) GetReciprocalContact()
    {
        return (_contact, _contact.CreateMirror());
    }
}