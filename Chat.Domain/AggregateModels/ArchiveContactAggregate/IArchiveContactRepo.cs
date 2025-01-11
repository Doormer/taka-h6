namespace Chat.Domain.AggregateModels.ArchiveContactAggregate;

/// <summary>
/// 
/// </summary>
public interface IArchiveContactRepo
{
    Task<Contact?> FindContactAsync(Guid userId, Guid contactUserId);
    void UpdateArchiveStatusAsync(Guid contactId, bool isArchived);
}