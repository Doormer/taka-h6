namespace Chat.Domain.AggregateModels.ContactArchivalAggregate;

/// <summary>
///     Repository for ContactArchival aggregate
/// </summary>
public interface IArchiveContactRepo : IRepository<ContactArchival>
{
    Task<ContactArchival?> FindContactAsync(Guid userId, Guid contactUserId);
    void UpdateArchiveStatus(ContactArchival contactArchival);
}