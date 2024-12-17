using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Chat.Infra.Data
{
    /// <summary>
    /// Factory for creating ChatDbContext instances at design time
    /// Used by EF Core tools for migrations
    /// </summary>
    public class ChatDbContextFactory : IDesignTimeDbContextFactory<ChatDbContext>
    {
        /// <summary>
        /// Creates a new instance of ChatDbContext
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>A new ChatDbContext instance</returns>
        public ChatDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChatDbContext>();
            optionsBuilder.UseMySql(
                "Server=localhost;Database=ChatDb;User=root;Password=yenngyenng;",
                ServerVersion.AutoDetect("Server=localhost;Database=ChatDb;User=root;Password=yenngyenng;")
            );

            return new ChatDbContext(optionsBuilder.Options);
        }
    }
} 