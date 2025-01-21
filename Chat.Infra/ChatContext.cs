using Chat.Domain.SeedWork;
using Chat.Domain.AggregateModels.ContactArchivalAggregate;
using Chat.Domain.AggregateModels.UnreadMessageAggregate;
using Contact = Chat.Domain.AggregateModels.ReciprocalContactAggregate.Contact;

using Chat.Infra.EntityConfigurations.ArchiveContactAggregate;
using ContactEntityTypeConfiguration =
    Chat.Infra.EntityConfigurations.ReciprocalContactAggregate.ContactEntityTypeConfiguration;
using UnreadMessageEntityTypeConfiguration =
    Chat.Infra.EntityConfigurations.UnreadMessageAggregate.UnreadMessageEntityTypeConfiguration;
using System.Diagnostics;
namespace Chat.Infra;

public class ChatContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    private IDbContextTransaction _currentTransaction;

    public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }

    public ChatContext(DbContextOptions<ChatContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        Debug.WriteLine("ChatContext::ctor ->" + GetHashCode());
    }

    public DbSet<Contact> ReciprocalContacts { get; set; }
    public DbSet<ContactArchival> ArchiveContacts { get; set; }
    public DbSet<UnreadMessage> UnreadMessages { get; set; }
    public DbSet<Domain.AggregateModels.ContactArchivalAggregate.Contact> Contacts { get; set; }


    public bool HasActiveTransaction => _currentTransaction != null;

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public IDbContextTransaction GetCurrentTransaction()
    {
        return _currentTransaction;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContactEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(
            new EntityConfigurations.ArchiveContactAggregate.ContactEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ArchiveContactEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UnreadMessageEntityTypeConfiguration());
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
}