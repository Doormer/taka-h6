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
            notification.Message.Id,
            notification.Message.SenderId,
            notification.Message.ReceiverId);

        // 这里可以添加其他逻辑，如：
        // 1. 发送推送通知
        // 2. 更新消息统计
        // 3. 触发其他业务流程

        return Task.CompletedTask;
    }
} 