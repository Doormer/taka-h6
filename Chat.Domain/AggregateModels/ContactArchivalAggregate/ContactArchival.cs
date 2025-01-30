using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ContactArchivalAggregate;

public sealed class ContactArchival : Entity, IAggregateRoot
{
    public ContactArchival(Guid userId, string userName, string avatarUrl)
    {
        UserId = userId;
        UserName = userName;
        AvatarUrl = avatarUrl;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public string AvatarUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    /// <summary>
    /// The contact that the user is archiving 
    /// </summary>
    /// <remarks>
    /// It must be a list in order for EF to establish the FK correctly
    /// </remarks>
    public List<Contact> Contacts { get; private set; }

    public void UpdateArchivedStatus(bool isArchived)
    {
        if (Contacts is null || Contacts.Count != 1)
        {
            throw new Exception("Contact is not found or more than one contact found");
        }
        Contacts.First().UpdateArchivedStatus(isArchived);
    }
}