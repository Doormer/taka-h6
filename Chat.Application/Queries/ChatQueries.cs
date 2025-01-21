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

    public async Task<Contact> GetArchivedContactsAsync(Guid userId)
    {
        try 
        {
            var query = context.contactsWithAllInfo.Where(c => c.UserId == userId && c.IsArchived);
            
            Console.WriteLine($"Executing query for userId: {userId}");
            Console.WriteLine($"SQL Query: {query.ToQueryString()}");
            
            var contact = await query.FirstOrDefaultAsync();
            
            Console.WriteLine($"Query result: {(contact == null ? "null" : "found")}");
            
            if (contact == null)
                throw new Exception($"Contact not found for userId: {userId}");
            
            return new Contact(contact.ContactUserId, contact.AvatarUrl, contact.lastMessage, contact.IsArchived);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetArchivedContactsAsync: {ex.Message}");
            throw;
        }
    }
}