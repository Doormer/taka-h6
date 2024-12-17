using Microsoft.EntityFrameworkCore;
using Chat.Domain.AggregatesModel.ContactAggregate;

namespace Chat.Infra.Data
{
    /// <summary>
    /// Database context for the chat application
    /// Handles the database connection and entity configurations
    /// </summary>
    public class ChatDbContext(DbContextOptions<ChatDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Gets the contacts database set
        /// </summary>
        public DbSet<Contact> Contacts => Set<Contact>();

        /// <summary>
        /// Configures the database model and relationships
        /// </summary>
        /// <param name="modelBuilder">The model builder instance</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contacts");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.UserName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.AvatarUrl).HasMaxLength(255);
                entity.Property(c => c.LastMessage).HasColumnType("text");
                entity.Property(c => c.CreatedTime);
                entity.Property(c => c.IsArchived);
            });
        }
    }
}