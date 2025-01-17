using Chat.Domain.AggregateModels.ContactArchivalAggregate;
using Chat.Domain.SeedWork;

namespace Chat.Infra.Repositories;

/// <summary>
/// Repository implementation for managing contact archival operations.
/// Handles database operations for archiving and retrieving contact information.
/// </summary>
public class ContactArchivalRepo(ChatContext context) : IArchiveContactRepo
{
    private readonly ChatContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public async Task<ContactArchival?> FindContactAsync(Guid userId, Guid contactUserId)
    {
        return await _context.ArchiveContacts
            .Include(o => o.Contact)
            .Where(ac => ac.UserId == userId)
            .Where(ac => ac.Contact != null && ac.Contact.ContactUserId == contactUserId)
            .FirstOrDefaultAsync();
    }

    public async Task<List<ContactArchival>> GetArchivedContactsAsync(Guid userId)
    {
        return await _context.ArchiveContacts
            .Include(o => o.Contact)
            .Where(ac => ac.UserId == userId)
            .Where(ac => ac.Contact != null && ac.Contact.IsArchived)
            .ToListAsync();
    }

    public async Task<List<ContactArchival>> GetActiveContactsAsync(Guid userId)
    {
        return await _context.ArchiveContacts
            .Include(o => o.Contact)
            .Where(ac => ac.UserId == userId)
            .Where(ac => ac.Contact != null && !ac.Contact.IsArchived)
            .ToListAsync();
    }

    public void UpdateArchiveStatus(ContactArchival contactArchival)
    {
        _context.ArchiveContacts.Update(contactArchival);
    }
}