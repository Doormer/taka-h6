namespace Chat.Domain.AggregateModels.ArchiveContactAggregate;

/// <summary>
/// 
/// </summary>
public interface IArchiveContactRepo : IRepository<ArchiveContact>
{
    Task<Contact?> FindContactAsync(Guid userId, Guid contactUserId);
    void UpdateArchiveStatus(Contact contact);
}