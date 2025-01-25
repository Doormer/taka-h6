using Chat.Domain.SeedWork;

namespace Chat.Domain.QueryEntities;

public sealed class Contact : Entity
{
    public Contact(Guid userId, Guid contactUserId)
    {
        UserId = userId;
        ContactUserId = contactUserId;
        AvatarUrl =
            "https://media.licdn.com/dms/image/v2/D5603AQETXS6tkEld2w/profile-displayphoto-shrink_200_200/profile-displayphoto-shrink_200_200/0/1720954054782?e=1743033600&v=beta&t=MW1lLGnY2XInXrrI1LeWQ_EMHapvNl3nFtQ7ZFjPQ98";
        lastMessage = "This bug belongs to Josie, all rights reserved.";
    }

    public Guid UserId { get; private set; }
    public Guid ContactUserId { get; private set; }
    public bool IsArchived { get; private set; }
    public string AvatarUrl { get; private set; }
    public string lastMessage { get; private set; }
}