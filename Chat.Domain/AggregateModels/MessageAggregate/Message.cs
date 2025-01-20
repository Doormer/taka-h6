using Chat.Domain.SeedWork;
using Chat.Domain.AggregateModels.MessageAggregate.Events;

namespace Chat.Domain.AggregateModels.MessageAggregate;

/// <summary>
/// Represents a message in the chat system.
/// This is the root aggregate that handles message lifecycle and state transitions.
/// </summary>
public class Message : Entity, IAggregateRoot
{
    public Guid Id { get; private set; }
    public Guid SenderId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public MessageContent Content { get; private set; }
    public MessageStatus Status { get; private set; }
    public DateTime CreatedTime { get; private set; }
    public DateTime? ReadTime { get; private set; }

    public Message(
        Guid senderId,
        Guid receiverId,
        object content,
        MessageType type,
        string? contentType = null)
    {
        Id = Guid.NewGuid();
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = new MessageContent(content, type, contentType);
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
} 