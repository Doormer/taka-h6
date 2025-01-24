using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ContactArchivalAggregate;

public sealed class ContactArchival : Entity, IAggregateRoot
{
    public ContactArchival(Guid userId, string username, string avatarUrl)
    {
        UserId = userId;
        Username = username;
        AvatarUrl = avatarUrl;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid UserId { get; private set; }
    public string Username { get; private set; }
    public string AvatarUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }
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