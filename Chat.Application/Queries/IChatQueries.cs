namespace Chat.Application.Queries;

public interface IChatQueries
{
    Task<List<Contact>> GetActiveContactsAsync(Guid userId);
    Task<Contact> GetArchivedContactsAsync(Guid userId);
}