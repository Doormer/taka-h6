using Microsoft.EntityFrameworkCore;
using Chat.Domain.AggregatesModel.ArchiveAggregate;
using Chat.Domain.AggregatesModel.ContactAggregate;
using Chat.Infra.Data;
using Chat.Infra.Repositories.Base;

namespace Chat.Infra.Repositories
{
    public class ArchiveRepository : Repository<Contact>, IArchiveRepository
    {
        public ArchiveRepository(ChatDbContext context) : base(context)
        {
        }

        public async Task<List<Contact>> GetUnarchivedContacts()
        {
            return await DbSet.Where(c => !c.IsArchived).ToListAsync();
        }

        public async Task<List<Contact>> GetArchivedContacts()
        {
            return await DbSet.Where(c => c.IsArchived).ToListAsync();
        }

        public async Task ToggleArchiveStatus(Guid contactId)
        {
            var contact = await GetById(contactId);
            if (contact != null)
            {
                if (contact.IsArchived)
                    contact.Unarchive();
                else
                    contact.Archive();
                await Context.SaveChangesAsync();
            }
        }
    }
} 