using Chat.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infra.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ChatContext _context;

    public UnitOfWork(ChatContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // 在保存前处理领域事件
        var domainEntities = _context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        // 保存更改
        await _context.SaveChangesAsync(cancellationToken);

        // 返回领域事件供后续处理
        return true;
    }
} 