using Chat.Domain.AggregateModels.ContactArchivalAggregate;

namespace Chat.Infra.EntityConfigurations.ArchiveContactAggregate;

/// <summary>
/// Entity Framework Core configuration for the Contact value object.
/// Maps contact properties to database columns and defines relationships.
/// </summary>
internal class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> contactConfiguration)
    {
        contactConfiguration.ToTable("contacts");

        contactConfiguration.Property(c => c.UserId).HasColumnName("UserId");
        contactConfiguration.Property(c => c.ContactUserId).HasColumnName("ContactUserId");
        contactConfiguration.Property(c => c.IsArchived).HasDefaultValue(false);

        // need to for entity to shared table
        contactConfiguration.HasOne<Domain.AggregateModels.ReciprocalContactAggregate.Contact>().WithOne()
                            .HasForeignKey<Contact>(e => e.Id);
    }
}