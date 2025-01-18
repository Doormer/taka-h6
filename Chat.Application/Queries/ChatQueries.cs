using Chat.Domain.QueryEntities;
using Chat.Infra;

namespace Chat.Application.Queries;

public class ChatQueries(ChatContext context)
    : IChatQueries
{
    public Task<Contact> GetContactsAsync(Guid userId)
    {
        return  context.contactsWithAllInfo.Where(c => c.UserId == userId).ToList();
    }

    public Task<Contact> GetArchivedContactsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}