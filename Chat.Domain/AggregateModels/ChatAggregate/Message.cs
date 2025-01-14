using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ChatAggregate;

public class Message : Entity
{
    public int Id { get; private set; }
    public int ChatId { get; private set; }
    public Guid SenderId { get; private set; }
    public required string Content { get; init; }
    public DateTime SentAt { get; private set; }
    public bool IsRead { get; private set; }

    public Chat Chat { get; private set; }

    protected Message() { }

    public Message(int chatId, Guid senderId, string content)
    {
        ChatId = chatId;
        SenderId = senderId;
        Content = content;
        SentAt = DateTime.UtcNow;
        IsRead = false;
    }

    public Message(int id, Guid senderId)
    {
        Id = id;
        SenderId = senderId;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}