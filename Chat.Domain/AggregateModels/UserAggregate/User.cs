using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.UserAggregate;

public class User : Entity, IAggregateRoot
{
    public Guid Id { get; private set; }
    
    public User(Guid id)
    {
        Id = id;
    }
} 