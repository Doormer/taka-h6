using Chat.Infra;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.Queries;

public class ChatQueries(ChatContext context)
    : IChatQueries
{
    public async Task<List<Contact>> GetActiveContactsAsync(Guid userId)
    {
        var contacts = await context.contactsWithAllInfo.Where(c => c.UserId == userId).ToListAsync();
        return contacts.Select(c => new Contact(c.ContactUserId, c.AvatarUrl, c.lastMessage, c.IsArchived)).ToList();
    }

    public Task<Contact> GetArchivedContactsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}