using MediatR;
namespace Chat.Domain.AggregateModels.MessageAggregate.Events;

public class MessageSentDomainEvent : INotification
{
    public Message Message { get; }

    public MessageSentDomainEvent(Message message)
    {
        Message = message;
    }
} 