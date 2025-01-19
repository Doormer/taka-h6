namespace Chat.Domain.AggregateModels.UnreadMessageAggregate;

/// <summary>
///     Repository for UnreadMessageAggregate
/// </summary>
public interface IUnreadMessageRepo : IRepository<UnreadMessage>
{
    Task<UnreadMessage?> GetUnreadMessageCount(Guid userId, Guid contactUserId);
}