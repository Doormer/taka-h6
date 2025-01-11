using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ArchiveContactAggregate;

public class ArchiveContact : Entity , IAggregateRoot
{
    public Guid user_id { get; private set; }
    public Contact? contact { get; private set; }

    public ArchiveContact(Guid userId, Guid contactUserId)
    {
        user_id = userId;
    }

    public void UpdateArchivedStatus(bool isArchived)
    {
        contact.UpdateArchivedStatus(isArchived);
    }
}