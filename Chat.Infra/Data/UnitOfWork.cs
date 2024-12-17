using System.Threading.Tasks;

namespace Chat.Infra.Data
{
    /// <summary>
    /// Implements the Unit of Work pattern for managing database transactions
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatDbContext _context;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class
        /// </summary>
        /// <param name="context">The database context</param>
        public UnitOfWork(ChatDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Saves all changes made in this context to the database
        /// </summary>
        /// <returns>True if any changes were saved, false otherwise</returns>
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Begins a new database transaction
        /// </summary>
        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Commits the current database transaction
        /// </summary>
        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        /// <summary>
        /// Rolls back the current database transaction
        /// </summary>
        public async Task RollbackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
} 