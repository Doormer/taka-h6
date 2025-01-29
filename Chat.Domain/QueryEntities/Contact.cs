using Chat.Domain.SeedWork;

namespace Chat.Domain.QueryEntities;

public sealed class Contact : Entity
{
    public Contact(Guid userId, Guid contactUserId)
    {
        UserId = userId;
        ContactUserId = contactUserId;
        AvatarUrl =
            "https://scontent-syd2-1.xx.fbcdn.net/v/t39.30808-6/461300323_2641222842735125_4681823755008426309_n.jpg?_nc_cat=100&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=8HboOGZaww4Q7kNvgEt4U-_&_nc_zt=23&_nc_ht=scontent-syd2-1.xx&_nc_gid=Ah9eazLO62G-SdnPxUJZliz&oh=00_AYA3h3E0QsQs5xWbfswuoFFCoH60UmPpJCU6rbn02AN2jg&oe=679A2484";
        lastMessage = "我是全公司最帅的男人！";
    }
    
    public Guid UserId { get; private set; }
    public Guid ContactUserId { get; private set; }
    public bool IsArchived { get; private set; }
    public string AvatarUrl { get; private set; }
    public string lastMessage { get; private set; }
}