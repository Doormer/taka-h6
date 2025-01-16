using MediatR;

namespace Chat.Domain.AggregateModels.MessageAggregate.Events;

/// <summary>
/// Domain event raised when a message is delivered to its recipient.
/// Used for tracking message delivery status and notifications.
/// </summary>
public class MessageDeliveredDomainEvent : INotification
{
    public Message Message { get; }

    public MessageDeliveredDomainEvent(Message message)
    {
        Message = message;
    }
} 