using MediatR;
namespace Chat.Domain.AggregateModels.MessageAggregate.Events;

/// <summary>
/// Domain event raised when a new message is sent.
/// Triggers notifications and message delivery process.
/// </summary>
public class MessageSentDomainEvent : INotification
{
    public Message Message { get; }

    public MessageSentDomainEvent(Message message)
    {
        Message = message;
    }
} 