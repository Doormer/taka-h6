using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.MessageAggregate;

/// <summary>
/// Repository interface for Message aggregate root.
/// Handles persistence operations for messages including querying message history,
/// unread messages and basic CRUD operations.
/// </summary>
public interface IMessageRepository : IRepository<Message>
{
    Task<Message?> FindByIdAsync(Guid messageId);
    Task<List<Message>> GetMessagesBetweenUsersAsync(
        Guid user1Id, 
        Guid user2Id, 
        DateTime? cursor,
        int limit
    );
    Task<List<Message>> GetUnreadMessagesAsync(Guid userId);
    void Add(Message message);
    void Update(Message message);
} 