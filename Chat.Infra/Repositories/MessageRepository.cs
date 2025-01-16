using Chat.Domain.AggregateModels.MessageAggregate;
using Chat.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Chat.Domain.SeedWork;

namespace Chat.Infra.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ChatContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public MessageRepository(ChatContext context)
    {
        _context = context;
    }

    public void Add(Message message)
    {
        _context.Messages.Add(message);
    }

    public void Update(Message message)
    {
        _context.Messages.Update(message);
    }

    public async Task<Message?> FindByIdAsync(Guid messageId)
    {
        return await _context.Messages.FindAsync(messageId);
    }

    public async Task<List<Message>> GetMessagesBetweenUsersAsync(Guid user1Id, Guid user2Id, int skip, int take)
    {
        return await _context.Messages
            .Where(m => 
                (m.SenderId == user1Id && m.ReceiverId == user2Id) ||
                (m.SenderId == user2Id && m.ReceiverId == user1Id))
            .OrderByDescending(m => m.CreatedTime)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<List<Message>> GetUnreadMessagesAsync(Guid userId)
    {
        return await _context.Messages
            .Where(m => m.ReceiverId == userId && m.Status != MessageStatus.Read)
            .OrderByDescending(m => m.CreatedTime)
            .ToListAsync();
    }
} 