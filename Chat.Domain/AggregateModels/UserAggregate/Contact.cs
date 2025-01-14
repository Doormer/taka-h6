using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.UserAggregate;

public class Contact : Entity
{
    public int Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ContactUserId { get; private set; }
    public bool IsArchived { get; private set; }

    public User User { get; private set; }

    protected Contact() { }

    public Contact(Guid userId, Guid contactUserId)
    {
        UserId = userId;
        ContactUserId = contactUserId;
        IsArchived = false;
    }

    public void Archive()
    {
        IsArchived = true;
    }

    public void Unarchive()
    {
        IsArchived = false;
    }
}