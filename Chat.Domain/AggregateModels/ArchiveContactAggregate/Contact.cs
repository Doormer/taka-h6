using System.Transactions;
using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.ArchiveContactAggregate;

public class Contact(Guid contactUserId, bool isArchived) : Entity
{
    public Guid ContactUserId { get; } = contactUserId;
    public bool IsArchived { get; private set; } = isArchived;

    public void UpdateArchivedStatus(bool isArchived)
    {
        IsArchived = isArchived;
    }
}