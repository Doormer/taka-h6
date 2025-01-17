using Chat.Domain.AggregateModels.MessageAggregate.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chat.Application.DomainEventHandlers;

public class MessageSentDomainEventHandler : INotificationHandler<MessageSentDomainEvent>
{
    private readonly ILogger<MessageSentDomainEventHandler> _logger;

    public MessageSentDomainEventHandler(ILogger<MessageSentDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MessageSentDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Message {MessageId} was sent from {SenderId} to {ReceiverId}",
            notification.MessageId,
            notification.SenderId,
            notification.ReceiverId);

        return Task.CompletedTask;
    }
}