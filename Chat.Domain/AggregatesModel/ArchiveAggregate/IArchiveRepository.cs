namespace Chat.Domain.AggregatesModel.ArchiveAggregate;

using Chat.Domain.AggregatesModel.ContactAggregate;

public interface IArchiveRepository
{
    Task<List<Contact>> GetUnarchivedContacts();
    Task<List<Contact>> GetArchivedContacts();
    Task ToggleArchiveStatus(Guid contactId);
}
