using Chat.Domain.SeedWork;

namespace Chat.Infra.Repositories;

public abstract class BaseRepository<T> where T : Entity, IAggregateRoot
{
    protected readonly ChatContext _context;

    protected BaseRepository(ChatContext context)
    {
        _context = context;
    }

    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public virtual void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }
} 