using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ArchiveContactAggregate;

public class ArchiveContact : Entity , IAggregateRoot
{
    public Guid UserId { get; private set; }
    
    public Contact? Contact { get; private set; }

    public ArchiveContact(Guid userId)
    {
        UserId = userId;
    }
    
    public void UpdateArchivedStatus(bool isArchived)
    {   
        if (Contact is null)
        {
            throw new Exception("Contact is null");
        }
        
        Contact.UpdateArchivedStatus(isArchived);
    }
}