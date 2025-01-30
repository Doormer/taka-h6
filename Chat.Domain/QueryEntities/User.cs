using Chat.Domain.SeedWork;

namespace Chat.Domain.QueryEntities;

public sealed class User : Entity
{
    public User(Guid userId, string userName, string avatarUrl)
    {
        UserId = userId;
        AvatarUrl = avatarUrl;
        UserName = userName;
    }

    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public string AvatarUrl { get; private set; }
}