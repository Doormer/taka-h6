using Chat.Domain.AggregateModels.UnreadMessageAggregate;
using Chat.Domain.SeedWork;

namespace Chat.Infra.Repositories;

public class UnreadMessageRepo(ChatContext context) : IUnreadMessageRepo
{
    private readonly ChatContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public async Task<int> GetUnreadMessageCount(int contactId)
    {
        return await _context.UnreadMessages
            .Where(m => m.ContactId == contactId && m.ReadTime == null)
            .CountAsync();
    }
}