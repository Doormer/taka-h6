namespace Chat.Domain.AggregateModels.UnreadMessageAggregate;

/// <summary>
///     Repository for UnreadMessageAggregate
/// </summary>
public interface IUnreadMessageRepo : IRepository<UnreadMessage>
{
    Task<int> GetUnreadMessageCount(int contactId);
}