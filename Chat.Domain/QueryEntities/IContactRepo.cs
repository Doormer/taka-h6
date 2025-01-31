namespace Chat.Domain.QueryEntities;

public interface IContactRepo
{
    Task<Contact> GetContactsAsync(Guid userId);
    Task<Contact> GetArchivedContactsAsync(Guid userId);
}