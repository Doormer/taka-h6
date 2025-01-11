using Chat.Domain.AggregateModels.ArchiveContactAggregate;

namespace Chat.Infra.EntityConfigurations.ArchiveContactAggregate;

internal class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> contactConfiguration)
    {
        contactConfiguration.ToTable("contacts");

        contactConfiguration.Ignore(c => c.DomainEvents);
        
        contactConfiguration.Property(c => c.UserId).HasColumnName("UserId");
        contactConfiguration.Property(c => c.ContactUserId).HasColumnName("ContactUserId");
        contactConfiguration.Property(c => c.IsArchived).HasDefaultValue(false);
        
        contactConfiguration.HasOne<Chat.Domain.AggregateModels.ReciprocalContactAggregate.Contact>().WithOne().HasForeignKey<Chat.Domain.AggregateModels.ArchiveContactAggregate.Contact>(e => e.Id);
        //todo 
        // add db constraint to not allow duplicate entries
        // add FK 
    }
}