using Chat.Domain.AggregateModels.MessageAggregate;
using Chat.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infra.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ChatContext _context;

    public MessageRepository(ChatContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Add(Message message)
    {
        _context.Messages.Add(message);
    }

    public async Task<Message?> FindByIdAsync(Guid messageId)
    {
        return await _context.Messages.FindAsync(messageId);
    }

    public void Update(Message message)
    {
        _context.Entry(message).State = EntityState.Modified;
    }

    public async Task<List<Message>> GetMessagesBetweenUsersAsync(Guid user1Id, Guid user2Id, DateTime? cursor, int limit)
    {
        var query = _context.Messages
            .Where(m =>
                (m.SenderId == user1Id && m.ReceiverId == user2Id) ||
                (m.SenderId == user2Id && m.ReceiverId == user1Id));

        if (cursor.HasValue)
        {
            query = query.Where(m => m.CreatedTime < cursor.Value);
        }

        return await query
            .OrderByDescending(m => m.CreatedTime)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<List<Message>> GetUnreadMessagesAsync(Guid userId)
    {
        return await _context.Messages
            .Where(m => m.ReceiverId == userId && m.Status == MessageStatus.Delivered)
            .OrderByDescending(m => m.CreatedTime)
            .ToListAsync();
    }
}