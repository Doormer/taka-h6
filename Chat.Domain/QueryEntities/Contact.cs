using System.IO.Compression;
using Chat.Domain.SeedWork;

namespace Chat.Domain.QueryEntities;

public class Contact : Entity
{
    public Guid UserId { get; private set; } 
    public Guid ContactUserId { get; private set; }
    public bool IsArchived { get; private set; }
    public string AvatarUrl { get; private set; }
    public string lastMessage { get; private set; }

    public Contact(Guid userId, Guid contactUserId, string avatarUrl)
    {
        UserId = userId;
        contactUserId = contactUserId;
        AvatarUrl = avatarUrl;
        lastMessage = "Josie doesnt want this anymore.";
    }
}