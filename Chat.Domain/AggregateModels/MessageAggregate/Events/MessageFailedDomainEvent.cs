using MediatR;

namespace Chat.Domain.AggregateModels.MessageAggregate.Events;

/// <summary>
/// Domain event raised when a message fails to be delivered or processed.
/// Includes the failure reason for error handling and logging.
/// </summary>
public class MessageFailedDomainEvent : INotification
{
    public Message Message { get; }
    public string Reason { get; }

    public MessageFailedDomainEvent(Message message, string reason)
    {
        Message = message;
        Reason = reason;
    }
} 