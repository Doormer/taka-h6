using Chat.Infra;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.Queries;

public class ChatQueries(ChatContext context)
    : IChatQueries
{
    public async Task<List<Contact>> GetActiveContactsAsync(Guid userId)
    {
        var contacts = await context.contactsWithAllInfo
            .Where(c => c.UserId == userId && c.IsArchived == false)
            .Join(
                context.Set<Domain.AggregateModels.ContactArchivalAggregate.ContactArchival>(),
                c => c.ContactUserId,
                u => u.UserId,
                (c, u) => new { Contact = c, User = u }
            )
            .ToListAsync();

        return contacts.Select(x => new Contact(
            x.Contact.ContactUserId,
            x.Contact.AvatarUrl,
            x.Contact.lastMessage,
            x.Contact.IsArchived)
        {
            UserName = x.User.Username
        }).ToList();
    }

    public async Task<List<Contact>> GetArchivedContactsAsync(Guid userId)
    {
        var contacts = await context.contactsWithAllInfo
            .Where(c => c.UserId == userId && c.IsArchived == true)
            .Join(
                context.Set<Domain.AggregateModels.ContactArchivalAggregate.ContactArchival>(),
                c => c.ContactUserId,
                u => u.UserId,
                (c, u) => new { Contact = c, User = u }
            )
            .ToListAsync();

        return contacts.Select(x => new Contact(
            x.Contact.ContactUserId,
            x.Contact.AvatarUrl,
            x.Contact.lastMessage,
            x.Contact.IsArchived)
        {
            UserName = x.User.Username
        }).ToList();
    }
}