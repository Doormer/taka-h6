using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ChatAggregate;

public class Chat : Entity, IAggregateRoot
{
    public int Id { get; private set; }
    public required string Name { get; init; }
    public DateTime CreatedAt { get; private set; }
    public ICollection<Message> Messages { get; private set; }

    protected Chat() { }

    public Chat(string name)
    {
        Name = name;
        CreatedAt = DateTime.UtcNow;
        Messages = new List<Message>();
    }

    public void UpdateMessageReadStatus(Guid userId, bool isRead)
    {
        var messages = Messages.Where(m => m.SenderId != userId);
        foreach (var message in messages)
        {
            message.UpdateReadStatus(isRead);
        }
    }

    public IEnumerable<Message> GetUnreadMessages(Guid userId)
    {
        return Messages.Where(m => m.SenderId != userId && !m.IsRead);
    }
}