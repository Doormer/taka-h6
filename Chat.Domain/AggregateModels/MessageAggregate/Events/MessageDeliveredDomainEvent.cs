using MediatR;

namespace Chat.Domain.AggregateModels.MessageAggregate.Events;

public class MessageDeliveredDomainEvent : INotification
{
    public Message Message { get; }

    public MessageDeliveredDomainEvent(Message message)
    {
        Message = message;
    }
} 