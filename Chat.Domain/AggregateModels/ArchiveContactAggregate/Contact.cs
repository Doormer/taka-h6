using System.Transactions;
using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ArchiveContactAggregate;

public class Contact(Guid userId, Guid contactUserId, bool isArchived) : Entity
{
    public Guid UserId { get; private set; } = userId;
    public Guid ContactUserId { get; } = contactUserId;
    public bool IsArchived { get; private set; } = isArchived;

    public void UpdateArchivedStatus(bool isArchived)
    {
        IsArchived = isArchived;
    }
}