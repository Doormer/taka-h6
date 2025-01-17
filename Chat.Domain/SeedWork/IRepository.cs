using Chat.Domain.SeedWork;

/// <summary>
/// Base interface for all repositories in the domain.
/// Provides access to the unit of work and defines generic repository operations.
/// </summary>
public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}