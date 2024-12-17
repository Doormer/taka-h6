using Chat.Domain.AggregatesModel.ContactAggregate;
using Chat.Infra.Data;
using Chat.Infra.Repositories.Base;

namespace Chat.Infra.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(ChatDbContext context) : base(context)
        {
        }

        public async Task DeleteContact(Guid id)
        {
            await Delete(id);
        }
    }
} 