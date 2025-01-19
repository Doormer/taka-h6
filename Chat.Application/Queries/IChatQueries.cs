using Chat.Domain.QueryEntities;

namespace Chat.Application.Queries;

public interface IChatQueries
{
    Task<List<Contact>> GetContactsAsync(Guid userId);
    Task<Contact> GetArchivedContactsAsync(Guid userId);
}