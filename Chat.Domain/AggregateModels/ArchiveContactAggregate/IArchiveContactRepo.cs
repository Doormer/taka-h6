namespace Chat.Domain.AggregateModels.ArchiveContactAggregate;

/// <summary>
/// Repository for ArchiveContact aggregate
/// </summary>
public interface IArchiveContactRepo : IRepository<ArchiveContact>
{
    Task<ArchiveContact?> FindContactAsync(Guid userId, Guid contactUserId);
    void UpdateArchiveStatus(ArchiveContact contact);
}