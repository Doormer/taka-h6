namespace Chat.Domain.AggregateModels.MessageAggregate.Services;

/// <summary>
/// Domain service for handling message-related business rules and validations.
/// Provides methods for content validation and user interaction checks.
/// </summary>
public class MessageDomainService
{
    public Task ValidateMessageContentAsync(MessageContent content)
    {
        throw new NotImplementedException();
    }

    public Task ValidateMessageSendingAsync(Guid senderId, Guid receiverId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CanUserSendMessageAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserBlockedAsync(Guid senderId, Guid receiverId)
    {
        throw new NotImplementedException();
    }
} 