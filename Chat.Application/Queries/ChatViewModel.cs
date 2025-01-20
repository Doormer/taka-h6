namespace Chat.Application.Queries;

public record Contact
{
    public Contact(Guid contactId, string avatarUrl, string lastMessage, bool isArchived)
    {
        ContactId = contactId;
        AvatarUrl = avatarUrl;
        LastMessage = lastMessage;
        IsArchived = isArchived;
    }

    public Guid ContactId { get; init; }
    public string AvatarUrl { get; init; }
    public string LastMessage { get; init; }
    public bool IsArchived { get; init; }
}