namespace Chat.Application.Queries;

public interface IChatQueries
{
    Task<List<ContactDto>> GetContactsAsync(string userId);
}

public class ContactDto
{
    public string id { get; set; } = string.Empty;
    public string userName { get; set; } = string.Empty;
    public string avatarUrl { get; set; } = string.Empty;
    public string lastMessage { get; set; } = string.Empty;
    public DateTime lastMessageCreatedTime { get; set; }
    public bool isArchived { get; set; }
    public bool isRead { get; set; }
}