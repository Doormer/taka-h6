using System.Runtime.Serialization;

namespace Chat.Application.Queries;

[DataContract]
public record Contact
{
    public Contact(Guid contactId, string avatarUrl, string userName, string lastMessage, bool isArchived, DateTime lastMessageCreatedTime, bool isRead)
    {
        Id = contactId;
        AvatarUrl = avatarUrl;
        IsArchived = isArchived;
        UserName = userName;
        LastMessage = lastMessage;
        LastMessageCreatedTime = lastMessageCreatedTime;
        IsRead = isRead;

    }

    [DataMember( Name = "id" )]
    public Guid Id { get; init; }

    [DataMember(Name = "userName")]
    public string UserName { get; init; }

    [DataMember( Name = "lastMessageCreatedTime" )]
    public DateTime LastMessageCreatedTime { get; init; }

    [DataMember(Name = "isRead")]
    public bool IsRead { get; init; }

    [DataMember(Name = "avatarUrl")]
    public string AvatarUrl { get; init; }

    [DataMember( Name = "lastMessage" )]
    public string LastMessage { get; init; }

    [DataMember( Name = "isArchived" )]
    public bool IsArchived { get; init; }
}