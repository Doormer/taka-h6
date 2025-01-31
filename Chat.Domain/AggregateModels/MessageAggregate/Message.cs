using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.MessageAggregate;

public class Message : Entity, IAggregateRoot
{
    public Message(Guid senderId, Guid receiverId, string content, DateTime sentTime)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = content;
        SentTime = sentTime;
        IsRead = false;
    }

    public Guid SenderId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public string Content { get; private set; }
    public DateTime SentTime { get; private set; }
    public bool IsRead { get; private set; }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}