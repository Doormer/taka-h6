using Chat.Domain.AggregateModels.MessageAggregate;
using Chat.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infra.Repositories;

public class MessageRepo : IMessageRepo
{
    private readonly ChatContext _context;

    public MessageRepo(ChatContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<int> GetUnreadMessageCount(Guid receiverId)
    {
        return await _context.Messages
            .Where(m => m.ReceiverId == receiverId && !m.IsRead)
            .CountAsync();
    }

    public async Task<List<Message>> GetMessageHistory(Guid senderId, Guid receiverId, int skip, int take)
    {
        return await _context.Messages
            .Where(m => 
                (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                (m.SenderId == receiverId && m.ReceiverId == senderId))
            .OrderByDescending(m => m.SentTime)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public void Add(Message message)
    {
        _context.Messages.Add(message);
    }

    public async Task<List<Message>> GetUnreadMessages(Guid receiverId)
    {
        return await _context.Messages
            .Where(m => m.ReceiverId == receiverId && !m.IsRead)
            .OrderBy(m => m.SentTime)
            .ToListAsync();
    }

    public void UpdateMessageStatus(Message message)
    {
        _context.Messages.Update(message);
    }
} 