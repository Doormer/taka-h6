using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.MessageAggregate;

public interface IMessageRepository : IRepository<Message>
{
    Task<Message?> FindByIdAsync(Guid messageId);
    Task<List<Message>> GetMessagesBetweenUsersAsync(Guid user1Id, Guid user2Id, int skip, int take);
    Task<List<Message>> GetUnreadMessagesAsync(Guid userId);
    void Add(Message message);
    void Update(Message message);
} 