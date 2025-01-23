namespace Chat.Domain.AggregateModels.MessageAggregate;

/// <summary>
///     Repository for MessageAggregate
/// </summary>
public interface IMessageRepo : IRepository<Message>
{
    Task<int> GetUnreadMessageCount(Guid receiverId);
    Task<List<Message>> GetMessageHistory(Guid senderId, Guid receiverId, int skip, int take);
    void Add(Message message);
    Task<List<Message>> GetUnreadMessages(Guid receiverId);
    void UpdateMessageStatus(Message message);
}