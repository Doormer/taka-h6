using MediatR;

namespace Chat.Domain.AggregateModels.MessageAggregate.Events;

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