using Chat.Domain.AggregateModels.ReciprocalContactAggregate;
using Chat.Domain.AggregateModels.UserAggregate;

namespace Chat.Infra.EntityConfigurations;

internal class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> contactConfiguration)
    {
        contactConfiguration.ToTable("contacts");

        contactConfiguration.Ignore(b => b.DomainEvents);

        contactConfiguration.Property(o => o.UserId)
            .IsRequired();
        
        contactConfiguration.Property(o => o.ContactUserId)
            .IsRequired();

        // Add unique constraint to prevent duplicate entries
        contactConfiguration.HasIndex(c => new { c.UserId, c.ContactUserId })
            .IsUnique();

        // Add foreign key constraints
        contactConfiguration.HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        contactConfiguration.HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.ContactUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}