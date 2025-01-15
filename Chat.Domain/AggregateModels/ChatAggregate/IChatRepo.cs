using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ChatAggregate;

public interface IChatRepo : IRepository<Chat>
{
    Task<Chat?> GetByIdAsync(int id);
    Task<IEnumerable<Chat>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Chat>> GetWithUnreadMessagesAsync(Guid userId);
    Task AddAsync(Chat chat);
    Task UpdateAsync(Chat chat);
    void UpdateMessageReadStatus(Chat chat);
}