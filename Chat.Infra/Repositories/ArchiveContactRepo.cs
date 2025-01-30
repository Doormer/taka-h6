using Chat.Domain.AggregateModels.ContactArchivalAggregate;
using Chat.Domain.SeedWork;

namespace Chat.Infra.Repositories;

public class ContactArchivalRepo(ChatContext context) : IArchiveContactRepo
{
    private readonly ChatContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public async Task<ContactArchival?> FindContactAsync(Guid userId, Guid contactUserId)
    {
        var ac = await _context.ArchiveContacts.Where(ac => ac.UserId == userId).FirstOrDefaultAsync();
        if (ac is not null) {
            await _context.Entry(ac).Collection(ac => ac.Contacts).Query()
                          .Where(c => c.ContactUserId == contactUserId).FirstOrDefaultAsync();
        }
        
        return ac;
    }

    public void UpdateArchiveStatus(ContactArchival contactArchival)
    {
        _context.ArchiveContacts.Update(contactArchival);
    }
}