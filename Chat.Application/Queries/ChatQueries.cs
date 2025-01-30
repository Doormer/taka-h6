using Chat.Infra;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.Queries;

public class ChatQueries(ChatContext context)
    : IChatQueries
{
    public async Task<List<Contact>> GetActiveContactsAsync(Guid userId)
    {
        var contacts = await context.rawContacts
            .Where(c => c.UserId == userId && c.IsArchived == false)
            .Join(
                context.rawUsers,
                c => c.ContactUserId,
                u => u.UserId,
                (c, u) => new { Contact = c, User = u }
            )
            .ToListAsync();
        return contacts.Select(x => new Contact(
            x.Contact.ContactUserId,
            x.User.AvatarUrl,
            x.User.UserName,
            x.Contact.lastMessage,
            x.Contact.IsArchived,
            // hardcoded for now
            DateTime.Now,
            false)
       ).ToList();
    }

    public async Task<List<Contact>> GetArchivedContactsAsync(Guid userId)
    {
        var contacts = await context.rawContacts
            .Where(c => c.UserId == userId && c.IsArchived == true)
            .Join(
                context.rawUsers,
                c => c.ContactUserId,
                u => u.UserId,
                (c, u) => new { Contact = c, User = u }
            )
            .ToListAsync();
        return contacts.Select(x => new Contact(
            x.Contact.ContactUserId,
            x.User.AvatarUrl,
            x.User.UserName,
            x.Contact.lastMessage,
            x.Contact.IsArchived,
            // hardcoded for now
            DateTime.Now,
            false)
        ).ToList();
    }
}