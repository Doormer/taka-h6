using Chat.Domain.AggregateModels.ArchiveContactAggregate;
using Chat.Domain.SeedWork;

namespace Chat.Infra.Repositories;

public class ArchiveContactRepo(ChatContext context)  : IArchiveContactRepo
{
    private readonly ChatContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;
    
    public async Task<Contact?> FindContactAsync(Guid userId, Guid contactUserId)
    {
        return await _context.archiveContacts.FindAsync(contactUserId, userId);
    }

    public void UpdateArchiveStatus(Contact contact)
    {
        _context.archiveContacts.Update(contact);
    }
}