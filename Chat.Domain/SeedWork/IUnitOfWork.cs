namespace Chat.Domain.SeedWork;

/// <summary>
/// Defines the contract for unit of work pattern implementation.
/// Manages transactions and ensures data consistency across repositories.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}