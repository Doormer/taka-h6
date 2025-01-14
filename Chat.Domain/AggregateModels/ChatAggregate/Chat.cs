using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ChatAggregate;

public class Chat : Entity, IAggregateRoot
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public ICollection<Message> Messages { get; private set; }

    protected Chat() { }

    public Chat(string name)
    {
        Name = name;
        CreatedAt = DateTime.UtcNow;
        Messages = new List<Message>();
    }

    public void AddMessage(string content, Guid senderId)
    {
        var message = new Message(this.Id, senderId, content);
        Messages.Add(message);
    }
}