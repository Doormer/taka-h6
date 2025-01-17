public interface IChatCacheService
{
    Task<List<MessageDto>> GetRecentMessagesAsync(Guid userId, Guid contactId);
    Task CacheMessagesAsync(Guid userId, Guid contactId, List<MessageDto> messages);
    Task InvalidateMessagesCacheAsync(Guid userId, Guid contactId);
} 