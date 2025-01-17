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
    public MessageContent Content { get; private set; } = null!;
    public MessageStatus Status { get; private set; }
    public DateTime CreatedTime { get; private set; }
    public DateTime? ReadTime { get; private set; }
    public MessageFailureReason? FailureReason { get; private set; }

    private Message() { }

    private Message(Guid senderId, Guid receiverId, MessageContent content)
    {
        Id = Guid.NewGuid();
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = content;
        Status = MessageStatus.Sent;
        CreatedTime = DateTime.UtcNow;

        AddDomainEvent(new MessageSentDomainEvent(Id, senderId, receiverId));
    }

    public static Message Create(Guid senderId, Guid receiverId, string content, MessageType type)
    {
        var messageContent = new MessageContent(content, type);
        return new Message(senderId, receiverId, messageContent);
    }

    public void MarkAsDelivered()
    {
        if (Status != MessageStatus.Sent)
            throw new InvalidOperationException($"Cannot mark message as delivered. Current status: {Status}");

        Status = MessageStatus.Delivered;
        AddDomainEvent(new MessageDeliveredDomainEvent(Id, ReceiverId));
    }

    public void MarkAsRead()
    {
        if (Status != MessageStatus.Delivered)
            throw new InvalidOperationException($"Cannot mark message as read. Current status: {Status}");

        Status = MessageStatus.Read;
        ReadTime = DateTime.UtcNow;
        AddDomainEvent(new MessageReadDomainEvent(Id, ReceiverId));
    }

    public void MarkAsFailed(MessageFailureReason reason)
    {
        if (Status != MessageStatus.Sent)
            throw new InvalidOperationException($"Cannot mark message as failed. Current status: {Status}");

        Status = MessageStatus.Failed;
        FailureReason = reason;
        AddDomainEvent(new MessageFailedDomainEvent(Id, reason.ToString()));
    }
}