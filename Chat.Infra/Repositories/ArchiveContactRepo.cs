using Chat.Domain.AggregateModels.ArchiveContactAggregate;
using Chat.Domain.SeedWork;

namespace Chat.Infra.Repositories;

public class ArchiveContactRepo(ChatContext context)  : IArchiveContactRepo
{
    private readonly ChatContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;
    
    public async Task<ArchiveContact?> FindContactAsync(Guid userId, Guid contactUserId)
    {

        return await _context.ArchiveContacts.Include(o => o.Contact).Where(ac => ac.UserId == userId)
                             .Where(ac => ac.Contact != null && ac.Contact.ContactUserId == contactUserId) // Check for null explicitly
                             .FirstOrDefaultAsync();
    }
    public void UpdateArchiveStatus(ArchiveContact contact)
    {
        _context.ArchiveContacts.Update(contact);
    }
}