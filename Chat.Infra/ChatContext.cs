using Chat.Domain.AggregateModels.UserAggregate;
using Chat.Domain.SeedWork;
using Chat.Infra.EntityConfigurations.ChatAggregate;
using Chat.Infra.EntityConfigurations.UserAggregate;
using System.Diagnostics;

namespace Chat.Infra;

public class ChatContext : DbContext, IUnitOfWork
{
    private readonly IMediator? _mediator;
    private IDbContextTransaction? _currentTransaction;

    public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }

    public ChatContext(DbContextOptions<ChatContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        Debug.WriteLine("ChatContext::ctor ->" + GetHashCode());
    }

    public DbSet<Domain.AggregateModels.ChatAggregate.Chat> Chats { get; set; }
    public DbSet<Domain.AggregateModels.ChatAggregate.Message> Messages { get; set; }
    public DbSet<User> Users { get; set; }

    public bool HasActiveTransaction => _currentTransaction != null;

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        if (_mediator != null)
        {
            await _mediator.DispatchDomainEventsAsync(this);
        }
        await base.SaveChangesAsync(cancellationToken);
        return true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MessageEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;
        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction)
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (HasActiveTransaction)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (HasActiveTransaction)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
}