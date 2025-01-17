public interface INotificationService
{
    Task NotifyMessageSentAsync(MessageDto message);
    Task NotifyMessageStatusChangedAsync(Guid messageId, MessageStatus newStatus);
    Task NotifyUserOnlineAsync(Guid userId);
    Task NotifyUserOfflineAsync(Guid userId);
} 