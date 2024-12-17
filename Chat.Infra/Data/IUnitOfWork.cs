using System.Threading.Tasks;

namespace Chat.Infra.Data
{
    /// <summary>
    /// Defines the contract for the Unit of Work pattern
    /// Manages database transactions and changes
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all pending changes to the database
        /// </summary>
        /// <returns>True if any changes were saved, false otherwise</returns>
        Task<bool> SaveChangesAsync();

        /// <summary>
        /// Begins a new database transaction
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// Commits the current transaction
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Rolls back the current transaction
        /// </summary>
        Task RollbackAsync();
    }
} 