using Chat.Domain.QueryEntities;
using Chat.Infra;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.Queries;

public class ChatQueries(ChatContext context)
    : IChatQueries
{
    public async Task<List<Contact>> GetContactsAsync(Guid userId)
    {
        return await context.contactsWithAllInfo.ToListAsync();
    }

    public Task<Contact> GetArchivedContactsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}