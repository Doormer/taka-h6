namespace Chat.Application.Queries;

public interface IChatQueries
{
    Task<List<Contact>> GetActiveContactsAsync(Guid userId);
    Task<List<Contact>> GetArchivedContactsAsync(Guid userId);
}