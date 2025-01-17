using Chat.Infra;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.Queries;

public class ChatQueries : IChatQueries
{
    private readonly ChatContext _context;

    public ChatQueries(ChatContext context)
    {
        _context = context;
    }

    public async Task<List<ContactDto>> GetContactsAsync(string userId)
    {
        // get all contacts of a user
        var contacts = await _context.Contacts
            .Where(c => c.UserId.ToString() == userId)
            .Select(c => new ContactDto
            {
                id = c.ContactUserId.ToString(),
                userName = "User Name",
                avatarUrl = "https://example.com/avatar.png",
                lastMessage = "last message",
                lastMessageCreatedTime = DateTime.UtcNow,
                isArchived = false,
                isRead = true
            })
            .ToListAsync();

        return contacts;
    }
}