using Chat.Domain.SeedWork;
using Chat.Domain.AggregateModels.MessageAggregate.Events;

namespace Chat.Domain.AggregateModels.MessageAggregate;
public enum MessageFailureReason
{
    ReceiverNotFound = 1,
    ReceiverBlocked = 2,
    ContentViolation = 3,
    ServerError = 4,
    Timeout = 5
}

/// <summary>
/// Represents a message in the chat system.
/// This is the root aggregate that handles message lifecycle and state transitions.
/// </summary>
public sealed class Message : Entity, IAggregateRoot
{
    public Guid SenderId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public MessageContent Content { get; private set; }
    public MessageStatus Status { get; private set; }
    public DateTime CreatedTime { get; private set; }
    public DateTime? ReadTime { get; private set; }
    public MessageFailureReason? FailureReason { get; private set; }

    // For EF Core
    protected Message() { }

    public Message(
        Guid senderId,
        Guid receiverId,
        string content,
        MessageType type)
    {
        Id = Guid.NewGuid();
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = new MessageContent(content, type);
        Status = MessageStatus.Sent;
        CreatedTime = DateTime.UtcNow;

        AddDomainEvent(new MessageSentDomainEvent(this));
    }

    public void MarkAsRead()
    {
        if (Status == MessageStatus.Failed)
            throw new InvalidOperationException("Cannot mark failed message as read");
            
        if (Status != MessageStatus.Read)
        {
            Status = MessageStatus.Read;
            ReadTime = DateTime.UtcNow;
            AddDomainEvent(new MessageReadDomainEvent(this));
        }
    }

    public void MarkAsDelivered()
    {
        if (Status == MessageStatus.Failed)
            throw new InvalidOperationException("Cannot mark failed message as delivered");
            
        if (Status == MessageStatus.Read)
            throw new InvalidOperationException("Cannot mark read message as delivered");
            
        if (Status == MessageStatus.Sent)
        {
            Status = MessageStatus.Delivered;
            AddDomainEvent(new MessageDeliveredDomainEvent(this));
        }
    }

    public void MarkAsFailed(MessageFailureReason reason)
    {
        if (Status == MessageStatus.Read)
            throw new InvalidOperationException("Cannot mark read message as failed");
            
        if (Status != MessageStatus.Failed)
        {
            Status = MessageStatus.Failed;
            FailureReason = reason;
            AddDomainEvent(new MessageFailedDomainEvent(this, reason));
        }
    }
} 