using Chat.Domain.SeedWork;
using Chat.Domain.AggregateModels.MessageAggregate.Events;

namespace Chat.Domain.AggregateModels.MessageAggregate;
public sealed class Message : Entity, IAggregateRoot
{
    public Guid SenderId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public required MessageContent Content { get; init; }
    public MessageStatus Status { get; private set; }
    public DateTime CreatedTime { get; private set; }
    public DateTime? ReadTime { get; private set; }


    private Message() { }

    public static Message Create(
        Guid senderId, 
        Guid receiverId, 
        string content,
        MessageType type)
    {
        var message = new Message
        {
            Id = Guid.NewGuid(),
            SenderId = senderId,
            ReceiverId = receiverId,
            Content = MessageContent.Create(content, type),
            Status = MessageStatus.Sent,
            CreatedTime = DateTime.UtcNow
        };

        message.AddDomainEvent(new MessageSentDomainEvent(message));
        return message;
    }

    public void MarkAsRead()
    {
        if (Status != MessageStatus.Read)
        {
            Status = MessageStatus.Read;
            ReadTime = DateTime.UtcNow;
            AddDomainEvent(new MessageReadDomainEvent(this));
        }
    }

    public void MarkAsDelivered()
    {
        if (Status == MessageStatus.Sent)
        {
            Status = MessageStatus.Delivered;
            AddDomainEvent(new MessageDeliveredDomainEvent(this));
        }
    }

    public void MarkAsFailed(string reason)
    {
        if (Status != MessageStatus.Failed)
        {
            Status = MessageStatus.Failed;
            AddDomainEvent(new MessageFailedDomainEvent(this, reason));
        }
    }
} 