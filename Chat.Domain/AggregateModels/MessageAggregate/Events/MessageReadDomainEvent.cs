using MediatR;
using Chat.Domain.AggregateModels.MessageAggregate;
namespace Chat.Domain.AggregateModels.MessageAggregate.Events;

public class MessageReadDomainEvent : INotification
{
    public Message Message { get; }

    public MessageReadDomainEvent(Message message)
    {
        Message = message;
    }
} 