namespace Chat.Domain.AggregateModels.MessageAggregate.Services;

public interface IMessageDomainService
{
    Task ValidateMessageContentAsync(MessageContent content);
    Task ValidateMessageSendingAsync(Guid senderId, Guid receiverId);
    Task<bool> CanUserSendMessageAsync(Guid userId);
    Task<bool> IsUserBlockedAsync(Guid senderId, Guid receiverId);
} 