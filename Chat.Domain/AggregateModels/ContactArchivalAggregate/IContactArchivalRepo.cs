namespace Chat.Domain.AggregateModels.ContactArchivalAggregate;

/// <summary>
///     Repository for ContactArchival aggregate
/// </summary>
public interface IArchiveContactRepo : IRepository<ContactArchival>
{
    Task<ContactArchival?> FindContactAsync(Guid userId, Guid contactUserId);
    Task<List<ContactArchival>> GetArchivedContactsAsync(Guid userId);
    Task<List<ContactArchival>> GetActiveContactsAsync(Guid userId);
    void UpdateArchiveStatus(ContactArchival contactArchival);
}