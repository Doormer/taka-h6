using MediatR;
using Chat.Domain.AggregateModels.MessageAggregate;
namespace Chat.Domain.AggregateModels.MessageAggregate.Events;

/// <summary>
/// Domain event raised when a message is read by its recipient.
/// Used for tracking read status and read receipts.
/// </summary>
public class MessageReadDomainEvent : INotification
{
    public Message Message { get; }

    public MessageReadDomainEvent(Message message)
    {
        Message = message;
    }
} 